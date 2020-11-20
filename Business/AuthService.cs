using Core.Helpers.Auth;
using Core.Helpers.Result;
using DataAccess;
using Entities;
using Entities.Dtos;

namespace Business
{
    public class AuthService : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IUserActivationDao _userActivationDao;

        public AuthService(IUserService userService, IUserActivationDao userActivationDao, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userActivationDao = userActivationDao;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (!userToCheck.Success)
                return new ErrorDataResult<User>(userToCheck.Message);

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password,
                userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
                return new ErrorDataResult<User>("Check your credentials");

            return new SuccessDataResult<User>(userToCheck.Data);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Email = userForRegisterDto.Email,
                Username = userForRegisterDto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = false
            };

            _userService.Add(user);

            var result = _userService.GetByMail(userForRegisterDto.Email);
            CodeHelper codeHelper = new CodeHelper();

            var userActivation = new UserActivation
            {
                UserId = result.Data.Id,
                ActivationCode = codeHelper.GenerateRandomCode(6)
            };

            _userActivationDao.Add(userActivation);

            return new SuccessDataResult<User>(user);
        }

        public IResult CheckUserExists(string email)
        {
            var result = _userService.GetByMail(email);
            if (result.Success)
                return new ErrorResult("User exists");

            return new SuccessResult();
        }

        public IResult ActivateAccount(int userId, string code)
        {
            var result = _userActivationDao.Get(a => a.UserId == userId);
            if (result == default)
                return new ErrorResult("User already activated");

            if (result.ActivationCode != code)
                return new ErrorResult("Wrong code");

            _userActivationDao.Delete(result);

            var userResult = _userService.GetById(userId);
            var user = userResult.Data;
            user.Status = true;
            _userService.Update(user);

            return new SuccessResult();
        }
    }
}
