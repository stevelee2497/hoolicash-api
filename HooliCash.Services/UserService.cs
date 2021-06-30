using AutoMapper;
using HooliCash.Core.Models;
using HooliCash.DTOs.User;
using HooliCash.IHelpers;
using HooliCash.IRepositories;
using HooliCash.IServices;
using HooliCash.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HooliCash.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenHelper _tokenHelper;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, ITokenHelper tokenHelper, IPasswordHelper passwordHelper, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _tokenHelper = tokenHelper;
            _passwordHelper = passwordHelper;
            _userRepository = unitOfWork.Users;
            _mapper = mapper;
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
                UserId = user.Id,
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
                UserId = user.Id,
                AccessToken = _tokenHelper.GenerateEncodedToken(user)
            };
        }

        public IEnumerable<UserDto> GetUsers()
        {
            return _userRepository.All().Select(_mapper.Map<UserDto>);
        }

        public void SeedDataUsers()
        {
            if (!_unitOfWork.Users.Any())
            {
                Register(new RegisterUserDto { Email = "admin", Password = "admin@123" });
            }
        }
    }
}
