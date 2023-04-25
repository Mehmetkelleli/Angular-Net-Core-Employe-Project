using Backend.Application.Abstractions;
using Backend.Domain.EntityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Backend.Application.Features.Employe.Query.OverTimeList
{
    public class OverTimeQueryListHandler : IRequestHandler<OverTimeQueryListRequest, OverTimeQueryListResponse>
    {
        private UserManager<Domain.EntityModels.Employe> _Employe;
        private IGenericRepository<PersonelTask> _Task;

        public OverTimeQueryListHandler(UserManager<Domain.EntityModels.Employe> employe, IGenericRepository<PersonelTask> task)
        {
            _Employe = employe;
            _Task = task;
        }

        public async Task<OverTimeQueryListResponse> Handle(OverTimeQueryListRequest request, CancellationToken cancellationToken)
        {
            var user =await _Employe.FindByNameAsync(request.UserName);

            var response = new OverTimeQueryListResponse();
            response.PersonelTasks = _Task.GetAll().Where(i => i.EmployeId == user.Id && i.WageHourState == false && i.WageHours !=0);
            return response;
        }
    }
}
