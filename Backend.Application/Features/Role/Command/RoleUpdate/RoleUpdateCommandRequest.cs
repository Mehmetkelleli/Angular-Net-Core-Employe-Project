using MediatR;

namespace Backend.Application.Features.Role.Command.RoleUpdate
{
    public class RoleUpdateCommandRequest:IRequest<RoleUpdateCommandResponse>
    {
        public string Id { get; set; }
        public double Wage { get; set; }
        public double OvertimePay { get; set; }
        public string Name { get; set; }
        public List<string>? NonUserId { get; set; }
        public List<string>? UserId { get; set; }
    }
}
