using Business;
using Core.Helpers.Auth;
using Core.Helpers.Mail;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

            CodeHelper codeHelper = new CodeHelper();
            await _mailer.SendMailAsync(userForRegisterDto.Email, "Welcome!", codeHelper.GenerateRandomCode(6));

            return Ok(result);
        }

        [HttpPost("activate")]
        [Authorize()]
        public IActionResult ActivateAccount(ActivationCodeDto activationCodeDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = int.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = _authService.ActivateAccount(userId, activationCodeDto.Code);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok();
        }
    }
}
