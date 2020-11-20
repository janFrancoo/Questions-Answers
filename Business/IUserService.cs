using Core.Helpers.Result;
using Entities;
using System.Collections.Generic;

namespace Business
{
    public interface IUserService
    {
        IDataResult<User> GetById(int id);
        IDataResult<User> GetByMail(string mail);
        IDataResult<List<User>> GetUsers();
        IResult Add(User user);
        IResult Update(User user);
        IResult ChangePassword(int userId, string currentPassword, string newPassword);
        List<OperationClaim> GetClaims(User user);
    }
}
