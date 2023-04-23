using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DataTransferObjects;
using WebApi.Filters;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [UseApiKey]
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(AuthenticationRegistrationHttpRequest request)
        {
            if (ModelState.IsValid)
            {
                if (await _authService.RegisterAsync(request))
                {
                    return Created("", null);
                }
            }
            return BadRequest();
        }

        [UseApiKey]
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(AuthenticationLoginHttpRequest request)
        {
            if (ModelState.IsValid)
            {
                var token = await _authService.LoginAsync(request);
                if (!string.IsNullOrEmpty(token))
                {
                    return Ok(token);
                }
            }

            return Unauthorized("Incorrect email or password");
        }
    }
}
