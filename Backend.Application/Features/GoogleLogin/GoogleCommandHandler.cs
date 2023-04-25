using Backend.Application.Abstractions;
using Backend.Application.Abstractions.Services;
using Backend.Application.Exceptions;
using Backend.Domain.EntityModels;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Backend.Application.Features.GoogleLogin
{
    public class GoogleCommandHandler : IRequestHandler<GoogleCommandRequest, GoogleCommandResponse>
    {
        private ITokenManager _Token;
        private UserManager<Domain.EntityModels.Employe> _Employe;
        private RoleManager<Domain.EntityModels.Role> _Role;
        private IConfiguration _Config;
        private IGenericRepository<PersonelTask> _Task;
        private IUserService _UserService;
        public GoogleCommandHandler(ITokenManager token, UserManager<Domain.EntityModels.Employe> employe, RoleManager<Domain.EntityModels.Role> role, IConfiguration config, IGenericRepository<PersonelTask> task, IUserService userService)
        {
            _Token = token;
            _Employe = employe;
            _Role = role;
            _Config = config;
            _Task = task;
            _UserService = userService;
        }

        public async Task<GoogleCommandResponse> Handle(GoogleCommandRequest request, CancellationToken cancellationToken)
        {
            ValidationSettings? settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>()
                { _Config["GoogleProvider:ClientId"] }
            };
            Payload payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

            UserLoginInfo userLoginInfo = new(request.Provider, payload.Subject, request.Provider);
            var user = await _Employe.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);
            bool result = user != null;
            if (user == null)
            {
                user = await _Employe.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new() { Id = Guid.NewGuid().ToString(), Email = payload.Email, UserName = payload.Email,ImagePath=request.PhotoUrl,Age=0,Name=payload.Name,LastName=payload.FamilyName };
                    IdentityResult createResult = await _Employe.CreateAsync(user);
                    result = createResult.Succeeded;
                }
                if (_Employe.Users.Count() == 1)
                {
                    if (await _Role.FindByNameAsync("Admin") == null)
                    {
                        await _Role.CreateAsync(new Backend.Domain.EntityModels.Role { Id = Guid.NewGuid().ToString(), Name = "Admin", Wage = 0, OvertimePay = 0 });
                        await _Employe.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await _Employe.AddToRoleAsync(user, "Admin");
                    }
                }
            }
            await _Employe.AddLoginAsync(user, userLoginInfo);

            var usersRole =await _Employe.GetRolesAsync(user);

            var token = _Token.GetAccessToken(60,user,usersRole);
            var response = new GoogleCommandResponse();
            response.Token = token.Token ;
            response.AccessToken = token.AccessToken;

            if (!await _UserService.UpdateRefreshToken(user.Id, token.AccessToken, token.ExpiresDate, 60))
            {
                throw new CustomException(new string[] { "update-token-error" });
            }

            var current = await _Task.GetSingleByIdAsync(i => i.CurrentDatetime.Year == DateTime.Now.Year && i.CurrentDatetime.Month == DateTime.Now.Month && i.CurrentDatetime.Day == DateTime.Now.Day);
            if (current == null)
            {
                current = new PersonelTask()
                {
                    Id = Guid.NewGuid(),
                    CurrentDatetime = DateTime.Now,
                    EmployeId = user.Id,
                    WageHours = 0,
                    WageHourState = false
                };
                await _Task.CreateAsync(current);
                await _Task.SaveAsync();
            }

            return response;
        }
    }
}
