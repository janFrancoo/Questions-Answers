using Core.DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess
{
    public class EFAnswerDao : EFRepositoryBase<Answer, QAContext>, IAnswerDao
    {
        public List<Answer> GetWithLikeCount(Expression<Func<Answer, bool>> filter = null)
        {
            using (var context = new QAContext())
            {
                return filter == null ? context.Set<Answer>()
                    .Join(
                        context.AnswerLikes,
                        answer => answer.Id,
                        answerLike => answerLike.AnswerId,
                        (answer, answerLike) => new
                        {
                            answer,
                            answerLike
                        })
                    .GroupBy(columns => new
                    {
                        columns.answer.Id,
                        columns.answer.QuestionId,
                        columns.answer.UserId,
                        columns.answer.AnswerText,
                        columns.answer.Date,
                    })
                    .Select(g => new Answer
                    {
                        Id = g.Key.Id,
                        QuestionId = g.Key.QuestionId,
                        UserId = g.Key.UserId,
                        AnswerText = g.Key.AnswerText,
                        Date = g.Key.Date,
                        LikeCount = g.Count()
                    }).ToList() :
                    context.Set<Answer>()
                    .Join(
                        context.AnswerLikes,
                        answer => answer.Id,
                        answerLike => answerLike.AnswerId,
                        (answer, answerLike) => new
                        {
                            answer,
                            answerLike
                        })
                    .GroupBy(columns => new
                    {
                        columns.answer.Id,
                        columns.answer.QuestionId,
                        columns.answer.UserId,
                        columns.answer.AnswerText,
                        columns.answer.Date,
                    })
                    .Select(g => new Answer
                    {
                        Id = g.Key.Id,
                        QuestionId = g.Key.QuestionId,
                        UserId = g.Key.UserId,
                        AnswerText = g.Key.AnswerText,
                        Date = g.Key.Date,
                        LikeCount = g.Count()
                    })
                    .Where(filter)
                    .ToList();
            }
        }
    }
}
