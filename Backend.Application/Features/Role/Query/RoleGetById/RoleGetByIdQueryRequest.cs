using MediatR;

namespace Backend.Application.Features.Role.Query.RoleGetById
{
    public class RoleGetByIdQueryRequest:IRequest<RoleGetByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
