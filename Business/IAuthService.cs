using Core.Helpers.Auth;
using Core.Helpers.Result;
using Entities;
using Entities.Dtos;

namespace Business
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult CheckUserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
