using Core.DataAccess;
using Entities;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class EFUserDao : EFRepositoryBase<User, QAContext>, IUserDao
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new QAContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name
                             };
                return result.ToList();
            }
        }
    }
}
