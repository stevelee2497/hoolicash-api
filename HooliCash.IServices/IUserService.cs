using HooliCash.DTOs.User;

namespace HooliCash.IServices
{
    public interface IUserService
    {
        LoginResponseDto Login(LoginUserDto loginUserDto);
        LoginResponseDto Register(RegisterUserDto registerUserDto);
        void SeedDataUsers();
    }
}
