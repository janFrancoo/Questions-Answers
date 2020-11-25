using Core.DataAccess;
using Entities;

namespace DataAccess
{
    public interface IQuestionDao: IEntityRepository<Question>
    {
    }
}
