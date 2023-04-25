using Backend.Application.Abstractions;
using Backend.Application.Exceptions;
using Backend.Domain.EntityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Backend.Application.Features.Employe.Command.OverTimeRequest
{
    public class OverTimeCommandHandler : IRequestHandler<OverTimeQueryRequest, OverTimeQueryResponse>
    {
        private UserManager<Domain.EntityModels.Employe> _Employe;
        private IGenericRepository<PersonelTask> _PersonelTask;

        public OverTimeCommandHandler(IGenericRepository<PersonelTask> personelTask, UserManager<Domain.EntityModels.Employe> employe)
        {
            _PersonelTask = personelTask;
            _Employe = employe;
        }

        public async Task<OverTimeQueryResponse> Handle(OverTimeQueryRequest request, CancellationToken cancellationToken)
        {
            var employe =await _Employe.FindByNameAsync(request.UserName);
            var personelTask =await _PersonelTask.GetSingleByIdAsync(i => i.EmployeId == employe.Id && i.CurrentDatetime.Year == DateTime.Now.Year && i.CurrentDatetime.Month == DateTime.Now.Month && i.CurrentDatetime.Day == DateTime.Now.Day,true);

            if (personelTask != null)
            {
                if (!personelTask.WageHourState && !personelTask.State)
                {
                    personelTask.WageHours = request.Hour;
                    await _PersonelTask.SaveAsync();
                }
                else
                {
                    throw new CustomException(new string[] { "payend-or-accepted" });
                }
            }
            return new OverTimeQueryResponse();
        }
    }
}
