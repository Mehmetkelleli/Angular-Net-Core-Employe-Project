using MediatR;
using Microsoft.AspNetCore.Http;

namespace Backend.Application.Features.Employe.Command
{
    public class UserCreateCommandRequest:IRequest<UserCreateCommandResponse>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public IFormFile Picture { get; set; }
    }
}
