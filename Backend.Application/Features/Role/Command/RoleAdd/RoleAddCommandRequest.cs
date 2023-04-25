using MediatR;

namespace Backend.Application.Features.Role.Command.RoleAdd
{
    public class RoleAddCommandRequest:IRequest<RoleAddCommandResponse>
    {
        public string Name { get; set; }
        public double Wage { get; set; }
        public double OvertimePay { get; set; }
    }
}
