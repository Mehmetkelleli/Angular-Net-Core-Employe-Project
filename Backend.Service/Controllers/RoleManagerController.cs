using Backend.Application.Exceptions;
using Backend.Application.Features.Role.Command.RoleAdd;
using Backend.Application.Features.Role.Command.RoleUpdate;
using Backend.Application.Features.Role.Query;
using Backend.Application.Features.Role.Query.RoleGetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class RoleManagerController : ControllerBase
    {
        private IMediator _Mediatr;

        public RoleManagerController(IMediator mediatr)
        {
            _Mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _Mediatr.Send(new RoleGetQueryRequest());
            return Ok(response.Roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var response = await _Mediatr.Send(new RoleGetByIdQueryRequest() { Id = id });
            return Ok(response.Role);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoleAddCommandRequest Model)
        {
            var response = await _Mediatr.Send(Model);
            return Ok(response.Role);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] RoleUpdateCommandRequest Model)
        {
            var response = await _Mediatr.Send(Model);
            return Ok(response.Role);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _Mediatr.Send(new RoleRemoveCommandRequest() { Id = id });
            return Ok(response.Role);
        }

    }
}
