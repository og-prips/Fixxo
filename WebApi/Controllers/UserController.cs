using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AuthService _authService;

        public UserController(AuthService authService)
        {
            _authService = authService;
        }

        [UseApiKey]
        [HttpGet]
        public async Task<IActionResult> Get(int userId)
        {
            //var user = await _authService.GetAsync(userId);
            //if (user != null)
            //{
            //    return Ok(user);
            //}

            return NotFound();
        }
    }
}
