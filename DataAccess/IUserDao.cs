using Core.DataAccess;
using Entities;

namespace DataAccess
{
    public interface IUserDao: IEntityRepository<User>
    {
    }
}
