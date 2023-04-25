using MediatR;

namespace Backend.Application.Features.Employe.Command.OverTimeRequest
{
    public class OverTimeQueryRequest:IRequest<OverTimeQueryResponse>
    {
        public int Hour { get; set; }
        public string UserName { get; set; }
    }
}
