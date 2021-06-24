using HooliCash.Core.Models;
using HooliCash.DTOs.User;
using HooliCash.IHelpers;
using HooliCash.IRepositories;
using HooliCash.IServices;
using HooliCash.Shared;
using System;

namespace HooliCash.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenHelper _tokenHelper;
        private readonly IPasswordHelper _passwordHelper;

        public UserService(IUnitOfWork unitOfWork, ITokenHelper tokenHelper, IPasswordHelper passwordHelper)
        {
            _unitOfWork = unitOfWork;
            _tokenHelper = tokenHelper;
            _passwordHelper = passwordHelper;
        }

        public LoginResponseDto Login(LoginUserDto loginUserDto)
        {
            var user = _unitOfWork.Users.FirstOrDefault(x => x.Email.ToUpper() == loginUserDto.Email.ToUpper());
            if (user == null)
            {
                throw new HooliCashException(Error.EmailNotExist);
            }

            var passwordHash = _passwordHelper.HashPassword(loginUserDto.Password, user.UpdatedOn);
            if (passwordHash != user.PasswordHash)
            {
                throw new HooliCashException(Error.WrongPassword);
            }

            return new LoginResponseDto
            {
                AccessToken = _tokenHelper.GenerateEncodedToken(user)
            };
        }

        public LoginResponseDto Register(RegisterUserDto registerUserDto)
        {
            if (_unitOfWork.Users.Any(x => x.Email.ToUpper() == registerUserDto.Email.ToUpper()))
            {
                throw new HooliCashException(Error.EmailExist);
            }

            var user = new User
            {
                DisplayName = registerUserDto.Email,
                Email = registerUserDto.Email,
                PasswordLastUpdatedTime = DateTimeOffset.Now,
                PasswordHash = _passwordHelper.HashPassword(registerUserDto.Password, DateTimeOffset.Now)
            };
            _unitOfWork.Users.Add(user);
            _unitOfWork.Complete();

            return new LoginResponseDto
            {
                AccessToken = _tokenHelper.GenerateEncodedToken(user)
            };
        }

        public void SeedDataUsers()
        {
            if (!_unitOfWork.Users.Any())
            {
                Register(new RegisterUserDto { Email = "admin", Password = "admin" });
            }
        }
    }
}
