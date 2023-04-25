using Backend.Application.Abstractions;
using Backend.Domain.EntityModels;
using MediatR;

namespace Backend.Application.Features.Employe.Command.Pay
{
    public class PayComandHandler : IRequestHandler<PayCommandRequest, PayCommandResponse>
    {
        private IGenericRepository<PersonelTask> _Task;

        public PayComandHandler(IGenericRepository<PersonelTask> task)
        {
            _Task = task;
        }

        public async Task<PayCommandResponse> Handle(PayCommandRequest request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(request.UserId))
            {
                foreach (var item in _Task.GetAll(true))
                {
                    item.State = true;
                }
                await _Task.SaveAsync();
            }
            else if(!string.IsNullOrEmpty(request.PersonelTaskId) && await _Task.GetByIdAsync(request.PersonelTaskId) != null)
            {
                var currentTask = await _Task.GetByIdAsync(request.PersonelTaskId, true);
                currentTask.State = true;
                await _Task.SaveAsync();
            }
            else if(!string.IsNullOrEmpty(request.UserId))
            {
                foreach (var item in _Task.GetAll(true).Where(i=>i.EmployeId == request.UserId))
                {
                    item.State = true;
                }
                await _Task.SaveAsync();
            }
            return new PayCommandResponse();

        }
    }
}
