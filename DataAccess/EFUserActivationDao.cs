using Core.DataAccess;
using Entities;

namespace DataAccess
{
    public class EFUserActivationDao: EFRepositoryBase<UserActivation, QAContext>, IUserActivationDao
    {
    }
}
