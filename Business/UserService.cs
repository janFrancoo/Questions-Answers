using Core.Helpers.Auth;
using Core.Helpers.Result;
using DataAccess;
using Entities;
using System.Collections.Generic;

namespace Business
{
    public class UserService : IUserService
    {
        private IUserDao _userDao;

        public UserService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public IResult Add(User user)
        {
            _userDao.Add(user);
            return new SuccessResult();
        }

        public IResult ChangePassword(int userId, string currentPassword, string newPassword)
        {
            var user = _userDao.Get(u => u.Id == userId);
            if (user == default)
                return new ErrorResult("User not found");

            if (!HashingHelper.VerifyPasswordHash(currentPassword, user.PasswordHash, user.PasswordSalt))
                return new ErrorResult("Check your credentials");

            HashingHelper.CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userDao.Update(user);

            return new SuccessResult();
        }

        public IDataResult<User> GetById(int id)
        {
            var result = _userDao.Get(u => u.Id == id);
            if (result == default)
                return new ErrorDataResult<User>("No such user");

            return new SuccessDataResult<User>(result);
        }

        public IDataResult<User> GetByMail(string mail)
        {
            var result = _userDao.Get(u => u.Email == mail);
            if (result == default)
                return new ErrorDataResult<User>("No such user");

            return new SuccessDataResult<User>(result);
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDao.GetClaims(user);
        }

        public IDataResult<List<User>> GetUsers()
        {
            return new SuccessDataResult<List<User>>(_userDao.GetList());
        }

        public IResult Update(User user)
        {
            _userDao.Update(user);
            return new SuccessResult();
        }
    }
}
