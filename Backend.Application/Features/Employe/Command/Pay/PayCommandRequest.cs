using MediatR;

namespace Backend.Application.Features.Employe.Command.Pay
{
    public class PayCommandRequest:IRequest<PayCommandResponse>
    {
        public string? UserId { get; set; }
        public string? PersonelTaskId { get; set; }
    }
}
