using Backend.Domain.EntityModels;

namespace Backend.Application.Features.Employe.Query.OverTimeList
{
    public class OverTimeQueryListResponse
    {
        public IQueryable<PersonelTask> PersonelTasks{ get; set; }
    }
}
