using Core.DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess
{
    public interface IAnswerDao: IEntityRepository<Answer>
    {
        List<Answer> GetWithLikeCount(Expression<Func<Answer, bool>> filter = null);
    }
}
