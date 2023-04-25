using Backend.Domain.EntityModels;

namespace Backend.Application.Features.Employe.Query.OverTimeListAdmin
{
    public class OverTimeListAdminQueryResponse
    {
        public IQueryable<PersonelTask> PersonelTasks { get; set; }
    }
}
