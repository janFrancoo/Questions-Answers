using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business;
using DataAccess;
using Entities;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.Middlewares;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private IQuestionService _questionService;
        private RedisService _redisService;
        private IClientConfiguration _clientConfiguration;

        public QuestionController(IQuestionService questionService, IClientConfiguration configuration, RedisService redisService)
        {
            _questionService = questionService;
            _redisService = redisService;
            _clientConfiguration = configuration;
        }

        [HttpGet("get-questions")]
        public IActionResult GetQuestions()
        {
            var result = _questionService.GetAll();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("get-question-by-id")]
        public async Task<IActionResult> GetQuestionById(int id) 
        {
            var questionFromCache = await _redisService.Get("q" + id);
            if (questionFromCache != null)
            {
                var question = JsonConvert.DeserializeObject<QuestionForDetailDto>(questionFromCache);
                return Ok(new
                {
                    Success = true,
                    Data = question
                });
            }

            var result = _questionService.GetByIdWithUser(id);
            if (result.Success)
            {
                await _redisService.Set("q" + id, JsonConvert.SerializeObject(result.Data));
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("get-question-by-user")]
        [Authorize()]
        public async Task<IActionResult> GetQuestionByUser(string userId)
        {
            int id;

            if (userId == null)
                id = int.Parse(_clientConfiguration.UserId);
            else
                id = int.Parse(userId);

            var questionsFromCache = await _redisService.Get("qu" + id);
            if (questionsFromCache != null)
            {
                var questions = JsonConvert.DeserializeObject<List<Question>>(questionsFromCache);
                return Ok(new
                {
                    Success = true,
                    Data = questions
                });
            }

            var result = _questionService.GetByUser(id);
            if (result.Success)
            {
                await _redisService.Set("qu" + id, JsonConvert.SerializeObject(result.Data));
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("get-question-by-date")]
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
            var userId = _clientConfiguration.UserId;
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
