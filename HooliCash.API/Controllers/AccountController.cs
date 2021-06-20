using HooliCash.DTOs.User;
using HooliCash.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HooliCash.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public ActionResult<LoginResponseDto> Login([FromBody] LoginUserDto loginUserDto)
        {
            var response = _userService.Login(loginUserDto);
            return response;
        }

        [HttpPost("register")]
        public ActionResult<LoginResponseDto> Register([FromBody] RegisterUserDto registerUserDto)
        {
            var response = _userService.Register(registerUserDto);
            return response;
        }
    }
}
