using Backend.Application.Dtos;

namespace Backend.Application.Features.Role.Query
{
    public class RoleGetRequestResponse
    {
        public IQueryable<RoleWithUserDto> Roles { get; set; }
    }
}
