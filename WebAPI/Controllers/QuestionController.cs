using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Business;
using DataAccess;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private IQuestionService _questionService;
        private RedisService _redisService;

        public QuestionController(IQuestionService questionService, RedisService redisService)
        {
            _questionService = questionService;
            _redisService = redisService;
        }

        [HttpGet("get-question-by-id")]
        [Authorize()]
        public async Task<IActionResult> GetQuestionById(int id) 
        {
            var questionFromCache = await _redisService.Get("q" + id);
            if (questionFromCache != null)
            {
                var question = JsonConvert.DeserializeObject<Question>(questionFromCache);
                return Ok(question);
            }

            var result = _questionService.GetById(id);
            if (result.Success)
            {
                await _redisService.Set("q" + id, JsonConvert.SerializeObject(result.Data));
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("get-question-by-user")]
        [Authorize()]
        public async Task<IActionResult> GetQuestionByUser(int userId)
        {
            var questionsFromCache = await _redisService.Get("qu" + userId);
            if (questionsFromCache != null)
            {
                var questions = JsonConvert.DeserializeObject<List<Question>>(questionsFromCache);
                return Ok(questions);
            }

            var result = _questionService.GetByUser(userId);
            if (result.Success)
            {
                await _redisService.Set("qu" + userId, JsonConvert.SerializeObject(result.Data));
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("get-question-by-date")]
        [Authorize()]
        public async Task<IActionResult> GetQuestionByDate(DateTime date)
        {
            var questionsFromCache = await _redisService.Get("qd" + date);
            if (questionsFromCache != null)
            {
                var questions = JsonConvert.DeserializeObject<List<Question>>(questionsFromCache);
                return Ok(questions);
            }

            var result = _questionService.GetByDate(date);
            if (result.Success)
            {
                await _redisService.Set("qd" + date, JsonConvert.SerializeObject(result.Data));
                return Ok(result.Data);
            }

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
