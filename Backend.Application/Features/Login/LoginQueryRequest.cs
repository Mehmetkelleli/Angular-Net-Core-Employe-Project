using MediatR;

namespace Backend.Application.Features.Login
{
    public class LoginQueryRequest:IRequest<LoginQueryResponse>
    {
        public string UserNameOrEMail{ get; set; }
        public string Password{ get; set; }
    }
}
