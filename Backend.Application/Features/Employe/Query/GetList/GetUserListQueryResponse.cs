using Backend.Application.Dtos;

namespace Backend.Application.Features.Employe.Query.GetList
{
    public class GetUserListQueryResponse
    {
        public IQueryable<EmployeWithWage> EmployeWithWages{ get; set; }
    }
}
