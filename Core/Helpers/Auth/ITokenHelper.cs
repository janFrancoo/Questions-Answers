using Entities;
using System.Collections.Generic;

namespace Core.Helpers.Auth
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
