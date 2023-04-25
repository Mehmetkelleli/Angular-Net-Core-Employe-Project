using Backend.Application.Abstractions;
using Backend.Application.Exceptions;
using Backend.Domain.EntityModels;
using MediatR;

namespace Backend.Application.Features.Employe.Command.OverTimesAccept
{
    public class OverTimeAcceptCommandHandler : IRequestHandler<OverTimeAcceptCommandRequest, OverTimeAcceptCommandResponse>
    {
        private IGenericRepository<PersonelTask> _Task;

        public OverTimeAcceptCommandHandler(IGenericRepository<PersonelTask> task)
        {
            _Task = task;
        }

        public async Task<OverTimeAcceptCommandResponse> Handle(OverTimeAcceptCommandRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.TaskId))
            {
                foreach (var item in _Task.GetAll(true))
                {
                    item.WageHourState = true;
                }
                await _Task.SaveAsync();
                return new OverTimeAcceptCommandResponse();
            }
            var personelData = await _Task.GetByIdAsync(request.TaskId,true);
            if(personelData != null)
            {
                personelData.WageHourState = true;
                await _Task.SaveAsync();
            }
            else
            {
                throw new CustomException(new string[] {"personel-task-not-found"});
            }
            return new OverTimeAcceptCommandResponse();
        }
    }
}
