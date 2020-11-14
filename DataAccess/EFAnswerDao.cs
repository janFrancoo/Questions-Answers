using Core.DataAccess;
using Entities;

namespace DataAccess
{
    public class EFAnswerDao: EFRepositoryBase<Answer, QAContext>, IAnswerDao
    {
    }
}
