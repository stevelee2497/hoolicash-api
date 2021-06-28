using HooliCash.DTOs.User;
using System.Collections.Generic;

namespace HooliCash.IServices
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetUsers();
        LoginResponseDto Login(LoginUserDto loginUserDto);
        LoginResponseDto Register(RegisterUserDto registerUserDto);
        void SeedDataUsers();
    }
}
