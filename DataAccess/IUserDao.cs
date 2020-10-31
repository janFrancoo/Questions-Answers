using Core.DataAccess;
using Entities;
using System.Collections.Generic;

namespace DataAccess
{
    public interface IUserDao: IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
    }
}
