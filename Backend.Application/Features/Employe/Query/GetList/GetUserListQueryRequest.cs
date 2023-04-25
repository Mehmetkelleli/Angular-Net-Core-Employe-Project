using MediatR;

namespace Backend.Application.Features.Employe.Query.GetList
{
    public class GetUserListQueryRequest:IRequest<GetUserListQueryResponse>
    {
        public string? UserId { get; set; }
    }
}
