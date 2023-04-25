using MediatR;

namespace Backend.Application.Features.Role.Command.RoleUpdate
{
    public class RoleRemoveCommandRequest:IRequest<RoleRemoveCommandResponse>
    {
        public string Id { get; set; }
    }
}
