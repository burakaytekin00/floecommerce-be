using ECommerce.Business;
using ECommerce.Entity.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserLoginDto userLoginDto)
        {
            var response = _loginService.Login(userLoginDto);
            if (!response.IsSuccess)
                return Unauthorized(response);
            return Ok(response);
        }
    }
} 