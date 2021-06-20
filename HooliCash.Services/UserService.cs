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
        private readonly IRepository<User> _userRepository;

        public UserService(IUnitOfWork unitOfWork, ITokenHelper tokenHelper, IPasswordHelper passwordHelper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = unitOfWork.Repository<User>();
            _tokenHelper = tokenHelper;
            _passwordHelper = passwordHelper;
        }

        public LoginResponseDto Login(LoginUserDto loginUserDto)
        {
            var user = _userRepository.FirstOrDefault(x => x.Email.ToUpper() == loginUserDto.Email.ToUpper());
            if (user == null)
            {
                throw new HooliCashException(Error.EmailNotExist);
            }

            var passwordHash = _passwordHelper.HashPassword(user.Email, user.UpdatedOn);
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
            if (_userRepository.Any(x => x.Email.ToUpper() == registerUserDto.Email.ToUpper()))
            {
                throw new HooliCashException(Error.EmailExist);
            }

            var user = new User
            {
                DisplayName = registerUserDto.Email,
                Email = registerUserDto.Email,
                PasswordLastUpdatedTime = DateTimeOffset.Now,
                PasswordHash = _passwordHelper.HashPassword(registerUserDto.Email, DateTimeOffset.Now)
            };
            _userRepository.Add(user);
            _unitOfWork.Complete();

            return new LoginResponseDto
            {
                AccessToken = _tokenHelper.GenerateEncodedToken(user)
            };
        }
    }
}
