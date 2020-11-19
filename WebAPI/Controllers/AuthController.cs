using Business;
using Core.Helpers.Mail;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private IMailer _mailer;

        public AuthController(IAuthService authService, IMailer mailer)
        {
            _authService = authService;
            _mailer = mailer;
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
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
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

            await _mailer.SendMailAsync(userForRegisterDto.Email, "Welcome!", "Test mail");

            return Ok(result);
        }
    }
}
