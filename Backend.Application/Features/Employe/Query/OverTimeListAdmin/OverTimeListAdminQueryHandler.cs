using Backend.Application.Abstractions;
using Backend.Domain.EntityModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Features.Employe.Query.OverTimeListAdmin
{
    public class OverTimeListAdminQueryHandler : IRequestHandler<OverTimeListAdminQueryRequest, OverTimeListAdminQueryResponse>
    {
        private IGenericRepository<PersonelTask> _Task;

        public OverTimeListAdminQueryHandler(IGenericRepository<PersonelTask> task)
        {
            _Task = task;
        }
        public async Task<OverTimeListAdminQueryResponse> Handle(OverTimeListAdminQueryRequest request, CancellationToken cancellationToken)
        {
            var taskList = _Task.GetAll().Include(i=>i.Employe).Where(i => !i.WageHourState && !i.State &&i.WageHours !=0);
            var response = new OverTimeListAdminQueryResponse();
            response.PersonelTasks = taskList;
            return response;
        }
    }
}
