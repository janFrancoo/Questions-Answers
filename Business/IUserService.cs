using Core.Helpers.Result;
using Entities;
using System.Collections.Generic;

namespace Business
{
    public interface IUserService
    {
        IDataResult<User> GetByMail(string mail);
        IDataResult<List<User>> GetUsers();
        IResult Add(User user);
        IResult Update(User user);
        List<OperationClaim> GetClaims(User user);
    }
}
