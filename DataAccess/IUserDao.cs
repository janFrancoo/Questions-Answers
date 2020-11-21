using Core.DataAccess;
using Entities;
using Entities.Dtos;
using System.Collections.Generic;

namespace DataAccess
{
    public interface IUserDao: IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        List<AnswerForAnswerLikeDto> GetLikes(int userId);
    }
}
