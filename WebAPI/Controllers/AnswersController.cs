using Business;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private IAnswerService _answerService;

        public AnswersController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet("get-answers-by-question")]
        [Authorize()]
        public IActionResult GetAnswersByQuestion(int questionId)
        {
            var result = _answerService.GetAnswersByQuestion(questionId);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }

        [HttpGet("get-answers-by-user")]
        [Authorize()]
        public IActionResult GetAnswersByUser(int userId)
        {
            var result = _answerService.GetAnswersByUser(userId);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }

        [HttpPost("add-answer")]
        [Authorize()]
        public IActionResult AddAnswer(Answer answer)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            answer.UserId = int.Parse(userId);

            var result = _answerService.Add(answer);
            if (result.Success)
                return Ok();

            return BadRequest(result.Message);
        }

        [HttpPut("update-answer")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateAnswer(Answer answer)
        {
            var result = _answerService.Update(answer);
            if (result.Success)
                return Ok();

            return BadRequest(result.Message);
        }

        [HttpDelete("delete-answer")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteAnswer(int id)
        {
            var result = _answerService.Delete(id);
            if (result.Success)
                return Ok();

            return BadRequest(result.Message);
        }
    }
}
