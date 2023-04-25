using Backend.Application.Abstractions;
using Backend.Application.Abstractions.Services;
using Backend.Application.Exceptions;
using Backend.Domain.EntityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Backend.Application.Features.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQueryRequest, LoginQueryResponse>
    {
        private UserManager<Backend.Domain.EntityModels.Employe> _Employe;
        private SignInManager<Backend.Domain.EntityModels.Employe> _SignIn;
        private ITokenManager _Token;
        private IGenericRepository<PersonelTask> _Task;
        private IUserService _UserService;

        public LoginQueryHandler(UserManager<Domain.EntityModels.Employe> employe, ITokenManager token, SignInManager<Domain.EntityModels.Employe> signIn, IGenericRepository<PersonelTask> task, IUserService userService)
        {
            _Employe = employe;
            _Token = token;
            _SignIn = signIn;
            _Task = task;
            _UserService = userService;
        }

        public async Task<LoginQueryResponse> Handle(LoginQueryRequest request, CancellationToken cancellationToken)
        {
            var user =await _Employe.FindByEmailAsync(request.UserNameOrEMail);
            if (user== null)
            {
                user = await _Employe.FindByNameAsync(request.UserNameOrEMail);
                if(user== null) {
                    throw new CustomException(new string[] { "user-not-found" });
                }
            }
            var result = await _SignIn.CheckPasswordSignInAsync(user, request.Password, true);
            if (!result.Succeeded)
            {
                throw new CustomException(new string[] { "password-or-username-false" });
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
            var usersRole =await _Employe.GetRolesAsync(user);

            var token = _Token.GetAccessToken(60,user,usersRole);
            if(!await _UserService.UpdateRefreshToken(user.Id, token.AccessToken, token.ExpiresDate, 60))
            {
                throw new CustomException(new string[] {"update-token-error"});
            }
            return new LoginQueryResponse { Token = token.Token,RefreshToken = token.AccessToken};
        }
    }
}
