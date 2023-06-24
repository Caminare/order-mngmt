using Microsoft.AspNetCore.Mvc;
using OrderMngmt.Business.Interfaces;
using OrderMngmt.Business.Models;

namespace OrderMngmt.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserAuthService _userAuthService;

        public UserAuthController(IUserAuthService userAuthService)
        {
            _userAuthService = userAuthService;
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserRegisterDTO user)
        {
            var result = await _userAuthService.RegisterAsync(user);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromQuery] UserLoginDTO user)
        {
            if (await _userAuthService.ValidateUserAsync(user))
            {
                var token = await _userAuthService.CreateTokenAsync();
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }
    }
}