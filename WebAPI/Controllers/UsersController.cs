using Business;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-user")]
        [Authorize()]
        public IActionResult GetUser(string email)
        {
            var result = _userService.GetByMail(email);
            
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
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

        [HttpPost("upload-avatar")]
        [Authorize()]
        public async Task<IActionResult> UploadAvatar(IFormCollection formData)
        {
            var files = HttpContext.Request.Form.Files;

            if (files.Count != 1)
                return BadRequest("Invalid file count");

            var file = files[0];
            var filePath = "Static/Avatars/" + file.FileName;
            var returnPath = "Media/Avatars/" + file.FileName;

            using var stream = System.IO.File.Create(filePath);
            await file.CopyToAsync(stream);

            return Ok(new { 
                Success = true,
                Data = new {
                    Path = returnPath
                }
            });
        }
    }
}
