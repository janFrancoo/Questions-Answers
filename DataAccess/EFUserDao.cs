using Core.DataAccess;
using Entities;
using Entities.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class EFUserDao : EFRepositoryBase<User, QAContext>, IUserDao
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new QAContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name
                             };
                return result.ToList();
            }
        }

        public List<AnswerForAnswerLikeDto> GetLikes(int userId)
        {
            using (var context = new QAContext())
            {
                var result = from answerLike in context.AnswerLikes
                             join answer in context.Answers
                             on answerLike.AnswerId equals answer.Id
                             join question in context.Questions
                             on answer.QuestionId equals question.Id
                             where answerLike.UserId == userId
                             select new AnswerForAnswerLikeDto
                             {
                                 QuestionId = question.Id,
                                 QuestionTitle = question.Title,
                                 AnswerText = answer.AnswerText
                             };
                return result.ToList();
            }
        }
    }
}
