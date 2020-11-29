﻿using Business;
using DataAccess;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private IAnswerService _answerService;
        private RedisService _redisService;

        public AnswersController(IAnswerService answerService, RedisService redisService)
        {
            _answerService = answerService;
            _redisService = redisService;
        }

        [HttpGet("get-answers-by-question")]
        public async Task<IActionResult> GetAnswersByQuestion(int questionId)
        {
            var answersFromCache = await _redisService.Get("aq" + questionId);
            if (answersFromCache != null)
            {
                var answers = JsonConvert.DeserializeObject<List<Answer>>(answersFromCache);
                return Ok(new
                {
                    Data = answers,
                    Success = true
                });
            }

            var result = _answerService.GetAnswersByQuestion(questionId);
            if (result.Success)
            {
                await _redisService.Set("aq" + questionId, JsonConvert.SerializeObject(result.Data));
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("get-answers-by-user")]
        [Authorize()]
        public async Task<IActionResult> GetAnswersByUser(string userId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var uId = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId == null)
                userId = uId;

            var answersFromCache = await _redisService.Get("au" + userId);
            if (answersFromCache != null)
            {
                var answers = JsonConvert.DeserializeObject<List<Answer>>(answersFromCache);
                return Ok(new { 
                    Data = answers,
                    Success = true
                });
            }

            var result = _answerService.GetAnswersByUser(int.Parse(userId));
            if (result.Success)
            {
                await _redisService.Set("au" + userId, JsonConvert.SerializeObject(result.Data));
                return Ok(result);
            }

            return BadRequest(result);
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
                return Ok(new { 
                    Success = true
                });

            return BadRequest(result);
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

        [HttpGet("like-answer")]
        [Authorize()]
        public IActionResult LikeAnswer(int answerId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int userId = int.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = _answerService.LikeAnswer(userId, answerId);
            if (result.Success)
                return Ok();

            return BadRequest(result.Message);
        }
    }
}
