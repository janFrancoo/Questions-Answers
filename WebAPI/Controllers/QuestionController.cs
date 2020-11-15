using System;
using System.Security.Claims;
using Business;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet("get-question-by-id")]
        [Authorize()]
        public IActionResult GetQuestionById(int id) 
        {
            var result = _questionService.GetById(id);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }

        [HttpGet("get-question-by-user")]
        [Authorize()]
        public IActionResult GetQuestionByUser(int userId)
        {
            var result = _questionService.GetByUser(userId);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }

        [HttpGet("get-question-by-date")]
        [Authorize()]
        public IActionResult GetQuestionByDate(DateTime date)
        {
            var result = _questionService.GetByDate(date);
            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }

        [HttpPost("add-question")]
        [Authorize()]
        public IActionResult AddQuestion(Question question)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            question.UserId = int.Parse(userId);

            var result = _questionService.Add(question);
            if (result.Success)
                return Ok();

            return BadRequest(result.Message);
        }

        [HttpPut("update-question")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateQuestion(Question question)
        {
            var result = _questionService.Update(question);
            if (result.Success)
                return Ok();

            return BadRequest(result.Message);
        }

        [HttpDelete("delete-question")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteQuestion(int id)
        {
            var result = _questionService.Delete(id);
            if (result.Success)
                return Ok();

            return BadRequest(result.Message);
        }
    }
}
