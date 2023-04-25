using MediatR;

namespace Backend.Application.Features.GoogleLogin
{
    public class GoogleCommandRequest:IRequest<GoogleCommandResponse>
    {
        public string IdToken{ get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public string PhotoUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Provider { get; set; }
    }
}

