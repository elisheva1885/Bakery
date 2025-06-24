using AutoMapper;
using Bakery;
using DTOs;
using Entities;
using Microsoft.Extensions.Logging;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Services
{
    public class UserServices : IUserServices
    {
        private readonly IUsersData usersData;//_usersData;
        private readonly IMapper autoMapping;//_autoMapping;
        private readonly ILogger<UserServices> logger;//_logger;
        public UserServices(IUsersData _usersData, IMapper _autoMapping, ILogger<UserServices> _logger)
        {
            autoMapping = _autoMapping;//_autoMapping =  autoMapping;
            usersData = _usersData;
            logger = _logger;
        }

        public int ValidatePasswordStrong(string password)
        {
            var zxcvbnResult = Zxcvbn.Core.EvaluatePassword(password);
            return zxcvbnResult.Score;
        }
        public async Task Register(RegisterUserDto registerUserDto)
        {
            try
            {
                if (registerUserDto.Username.Length < 3)
                    throw new HttpStatusException(400, "username must be at least 3 letters");
                if(registerUserDto.Username.Equals(registerUserDto.Password))
                    throw new HttpStatusException(400, "password isn't safe");
                if (ValidatePasswordStrong(registerUserDto.Password) > 2)
                {
                    User user = autoMapping.Map<User>(registerUserDto);
                    await usersData.Register(user);
                }
                else
                {
                    throw new HttpStatusException(400, "password is not strong enough");
                }
            }
            catch (HttpStatusException e)
            {

                throw new HttpStatusException(e.StatusCode, e.Message);
            }

        }

        public async Task<UserDto> Login(LoginUserDto loginUserDto)
        {
            try
            {
                User loginUser = autoMapping.Map<User>(loginUserDto);
                User user = await usersData.Login(loginUser);
                logger.LogInformation("User logged in: {0} {1} ", user.Username, user.Password);

                UserDto userDto = autoMapping.Map<UserDto>(user);
                return userDto;
            }
            catch (HttpStatusException e)
            {

                throw new HttpStatusException(e.StatusCode, e.Message);
            }
        }

        public async Task update(int id, RegisterUserDto userDto)//Update
        {
            try
            {
                User user = autoMapping.Map<User>(userDto);
                await usersData.Update(id, user);
            }
            catch (HttpStatusException e)
            {

                throw new HttpStatusException(e.StatusCode, e.Message);
            }
        }

        public async Task<UserDto> getUserId(int id)//GetUserId
        {
            User user = await usersData.getUserId(id);
            return autoMapping.Map<UserDto>(user);
        }

        public async Task<List<UserDto>> getUsers()//GetUsers
        {
            List<User> users = await usersData.getUsers();
            return autoMapping.Map<List<UserDto>>(users);
        }
    }
}
