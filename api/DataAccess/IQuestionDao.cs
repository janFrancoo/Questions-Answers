using Core.DataAccess;
using Entities;
using Entities.Dtos;
using System;
using System.Linq.Expressions;

namespace DataAccess
{
    public interface IQuestionDao: IEntityRepository<Question>
    {
        QuestionForDetailDto GetWithUser(Expression<Func<QuestionForDetailDto, bool>> filter);
    }
}
