using MediatR;

namespace Backend.Application.Features.RefreshToken
{
    public class LoginRefreshTokenCommandRequest:IRequest<LoginRefreshTokenCommandResponse>
    {
        public string RefreshToken { get; set; }
    }
}
