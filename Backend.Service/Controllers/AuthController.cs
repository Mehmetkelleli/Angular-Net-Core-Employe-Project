using Backend.Application.Features.Employe.Command;
using Backend.Application.Features.GoogleLogin;
using Backend.Application.Features.Login;
using Backend.Application.Features.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IMediator _Mediator;

        public AuthController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] UserCreateCommandRequest Model)
        {
            await _Mediator.Send(Model);
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginQueryRequest Model)
        {
            var response =await _Mediator.Send(Model);
            return Ok(new {Token = response.Token,RefreshToken=response.RefreshToken});
        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> Google(GoogleCommandRequest Model)
        {
            var response = await _Mediator.Send(Model);
            return Ok(new { Token = response.Token ,RefreshToken = response.AccessToken});
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginRefreshToken([FromBody] LoginRefreshTokenCommandRequest model)
        {
            var response = await _Mediator.Send(model);
            return Ok(new {Token = response.Token,RefreshToken = response.Token.AccessToken});
        }
    }
}
