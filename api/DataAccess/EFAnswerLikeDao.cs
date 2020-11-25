using Core.DataAccess;
using Entities;

namespace DataAccess
{
    public class EFAnswerLikeDao: EFRepositoryBase<AnswerLike, QAContext>, IAnswerLikeDao
    {
    }
}
