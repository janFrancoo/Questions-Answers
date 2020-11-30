using Core.DataAccess;
using Entities;
using Entities.Dtos;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess
{
    public class EFQuestionDao: EFRepositoryBase<Question, QAContext>, IQuestionDao
    {
        public QuestionForDetailDto GetWithUser(Expression<Func<QuestionForDetailDto, bool>> filter)
        {
            using (var context = new QAContext())
            {
                // return context.Set<Question>().SingleOrDefault(filter);
                return context.Set<Question>()
                    .Join(
                        context.Users,
                        question => question.UserId,
                        user => user.Id,
                        (question, user) => new QuestionForDetailDto
                        {
                            Question = question,
                            UserId = user.Id,
                            Username = user.Username,
                            Avatar = user.Avatar
                        })
                    .SingleOrDefault(filter);
            }
        }
    }
}
