using Core.DataAccess;
using Entities;

namespace DataAccess
{
    public class EFQuestionDao: EFRepositoryBase<Question, QAContext>, IQuestionDao
    {
    }
}
