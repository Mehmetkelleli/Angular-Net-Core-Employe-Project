using MediatR;

namespace Backend.Application.Features.Employe.Query.OverTimeList
{
    public class OverTimeQueryListRequest:IRequest<OverTimeQueryListResponse>
    {
        public string? UserName{ get; set; }
    }
}
