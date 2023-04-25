using Backend.Application.Abstractions;
using Backend.Application.Abstractions.Services;
using Backend.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Features.RefreshToken
{
    public class LoginRefreshTokenCommandHandler : IRequestHandler<LoginRefreshTokenCommandRequest, LoginRefreshTokenCommandResponse>
    {
        private UserManager<Domain.EntityModels.Employe> _Employe;
        private IUserService _UserService;
        private RoleManager<Domain.EntityModels.Role> _RoleManager;
        private ITokenManager _Token;

        public LoginRefreshTokenCommandHandler(UserManager<Domain.EntityModels.Employe> employe, IUserService userService, RoleManager<Domain.EntityModels.Role> roleManager, ITokenManager token)
        {
            _Employe = employe;
            _UserService = userService;
            _RoleManager = roleManager;
            _Token = token;
        }

        public async Task<LoginRefreshTokenCommandResponse> Handle(LoginRefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _Employe.Users.FirstOrDefaultAsync(i => i.RefreshToken == request.RefreshToken);
            var response = new LoginRefreshTokenCommandResponse();

            if (user != null && user.RefreshTokenEndDate > DateTime.UtcNow)
            {
                var roles = await _Employe.GetRolesAsync(user);
                var token = _Token.GetAccessToken(1,user,roles);
                if (token == null)
                {
                    throw new CustomException(new string[] { "token-error" });
                }
                if(!await _UserService.UpdateRefreshToken(user.Id, token.AccessToken, token.ExpiresDate, 1))
                {
                    throw new CustomException(new string[] { "token-error" });
                }
                response.Token = token;
            }
            else
            {
                throw new CustomException(new string[] { "token-error" });
            }
            return response;
        }
    }
}
