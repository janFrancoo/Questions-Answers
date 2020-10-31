using Core.DataAccess;
using Entities;

namespace DataAccess
{
    public class EFUserDao: EFRepositoryBase<User, QAContext>, IUserDao
    {
    }
}
