using Business;
using DataAccess;
using Entities;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private RedisService _redisService;

        public UsersController(IUserService userService, RedisService redisService)
        {
            _userService = userService;
            _redisService = redisService;
        }

        [HttpGet("get-user-by-id")]
        [Authorize()]
        public async Task<IActionResult> GetUserById(string id)
        {
            int userId;

            if (id == null)
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                userId = int.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            else
                userId = int.Parse(id);

            var userFromCache = await _redisService.Get("u" + userId);
            if (userFromCache != null)
            {
                var user = JsonConvert.DeserializeObject<User>(userFromCache);
                user.PasswordHash = null;
                user.PasswordSalt = null;
                return Ok(new
                {
                    Success = true,
                    Data = user
                });
            }

            var result = _userService.GetById(userId);
            if (result.Success)
            {
                result.Data.PasswordSalt = null;
                result.Data.PasswordHash = null;
                await _redisService.Set("u" + userId, JsonConvert.SerializeObject(result.Data));
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("get-user-by-email")]
        [Authorize()]
        public async Task<IActionResult> GetUserByMail(string email)
        {
            var userFromCache = await _redisService.Get("u" + email);
            if (userFromCache != null)
            {
                var user = JsonConvert.DeserializeObject<User>(userFromCache);
                user.PasswordHash = null;
                user.PasswordSalt = null;
                return Ok(new
                {
                    Success = true,
                    Data = user
                });
            }

            var result = _userService.GetByMail(email);
            if (result.Success)
            {
                result.Data.PasswordHash = null;
                result.Data.PasswordSalt = null;
                await _redisService.Set("u" + email, JsonConvert.SerializeObject(result.Data));
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("get-all-users")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            var result = _userService.GetUsers();

            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }

        [HttpPut("update-user")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateUser(User user)
        {
            var result = _userService.Update(user);
            if (result.Success)
                return Ok();

            return BadRequest(result.Message);
        }

        [HttpPost("update-avatar")]
        [Authorize()]
        public async Task<IActionResult> UpdateAvatar(IFormCollection formData)
        {
            var files = HttpContext.Request.Form.Files;
            if (files.Count != 1)
                return BadRequest("Invalid file count");

            var file = files[0];
            if (!file.ContentType.Split("/")[0].Equals("image"))
                return BadRequest("Invalid file format");

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = int.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);

            string fileName = userId + "." + file.ContentType.Split("/")[1];
            string filePath = "Static/Avatars/" + fileName;

            using var stream = System.IO.File.Create(filePath);
            await file.CopyToAsync(stream);

            var user = _userService.GetById(userId).Data;
            user.Avatar = fileName;
            _userService.Update(user);
            await _redisService.Set("u" + userId, JsonConvert.SerializeObject(user));

            return Ok(new
            {
                Success = true,
                Data = new {
                    Avatar = fileName
                }
            });
        }

        [HttpPut("change-password")]
        [Authorize()]
        public IActionResult ChangePassword(PasswordUpdateDto passwordUpdateDto)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = int.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = _userService.ChangePassword(userId, passwordUpdateDto.CurrentPassword, passwordUpdateDto.NewPassword);
            if (result.Success)
                return Ok();

            return BadRequest(result.Message);
        }

        [HttpGet("get-likes")]
        [Authorize()]
        public IActionResult GetLikes()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = int.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = _userService.GetLikes(userId);
            return Ok(result);
        }
    }
}
