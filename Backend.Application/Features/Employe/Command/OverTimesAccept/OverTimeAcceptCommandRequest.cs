using MediatR;

namespace Backend.Application.Features.Employe.Command.OverTimesAccept
{
    public class OverTimeAcceptCommandRequest:IRequest<OverTimeAcceptCommandResponse>
    {
        public string? TaskId { get; set; }
    }
}
