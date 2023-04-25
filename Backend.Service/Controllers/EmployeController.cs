using Backend.Application.Features.Employe.Command.OverTimeRequest;
using Backend.Application.Features.Employe.Command.OverTimesAccept;
using Backend.Application.Features.Employe.Command.Pay;
using Backend.Application.Features.Employe.Query.GetList;
using Backend.Application.Features.Employe.Query.OverTimeList;
using Backend.Application.Features.Employe.Query.OverTimeListAdmin;
using Backend.Domain.EntityModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeController : ControllerBase
    {
        private IMediator _Mediator;
        private UserManager<Employe> _Employe;

        public EmployeController(IMediator mediator, UserManager<Employe> employe)
        {
            _Mediator = mediator;
            _Employe = employe;
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get([FromRoute] string? id)
        {

            var response = await _Mediator.Send(new GetUserListQueryRequest() { UserId = id});
            return Ok(response.EmployeWithWages);
        
        }
        
        [HttpGet("[action]/{id?}")]
        public async Task<IActionResult> Pay([FromRoute] string? id)
        {
            await _Mediator.Send(new PayCommandRequest() { UserId = id,PersonelTaskId=id });
            return Ok();
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> OverTimeRequestList()
        {
            var response = await _Mediator.Send(new OverTimeQueryListRequest(){ UserName=User.Identity.Name});
            return Ok(response.PersonelTasks);
        } 
        [HttpGet("[action]/{hour}")]
        public async Task<IActionResult> OverTimeRequest([FromRoute] int hour)
        {
            await _Mediator.Send(new OverTimeQueryRequest() { Hour = hour,UserName = User.Identity.Name });
            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> OverTimeList()
        {
            var response = await _Mediator.Send(new OverTimeListAdminQueryRequest());
            return Ok(response.PersonelTasks);
        }

        [HttpGet("[action]/{id?}")]
        public async Task<IActionResult> OverTimeAccept(string? id)
        {
            var response = await _Mediator.Send(new OverTimeAcceptCommandRequest() { TaskId=id});
            return Ok();
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEmployeData()
        {
            Console.WriteLine(User.Identity.Name);
            var user = await _Employe.FindByNameAsync(User.Identity.Name);
            var response = await _Mediator.Send(new GetUserListQueryRequest() { UserId = user.Id});

            return Ok(response.EmployeWithWages);

        }
    }
}
