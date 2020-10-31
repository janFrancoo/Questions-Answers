using Business;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
                return BadRequest(userToLogin);

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var checkUserExists = _authService.CheckUserExists(userForRegisterDto.Email);
            if (!checkUserExists.Success)
                return BadRequest(checkUserExists);

            var userToRegister = _authService.Register(userForRegisterDto);
            if (!userToRegister.Success)
                return BadRequest(userToRegister.Message);

            var result = _authService.CreateAccessToken(userToRegister.Data);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
