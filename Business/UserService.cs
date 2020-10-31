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

        public IDataResult<User> GetByMail(string mail)
        {
            return new SuccessDataResult<User>(_userDao.Get(u => u.Email == mail));
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
