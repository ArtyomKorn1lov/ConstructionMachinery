using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using Application.IServices;
using Domain.IRepositories;
using Domain.Entities;
using Application.CommandsConverters;
using System.Security.Cryptography;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public string HashPassword(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        public async Task<bool> Create(UserCreateCommand user)
        {
            try
            {
                if(user != null)
                {
                    user.Created = DateTime.Now;
                    await _accountRepository.Create(UserCommandConverter.UserCreateCommandConvertToUserEntity(user));
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<UserCommand> GetById(int id)
        {
            try
            {
                UserCommand user = UserCommandConverter.UserEntityConvertToUserCommand(await _accountRepository.GetById(id));
                if (user == null)
                    return null;
                return user;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> GetLoginResult(string email, string password)
        {
            try
            {
                if (email == null || password == null)
                {
                    return false;
                }
                User user = await _accountRepository.GetLoginModel(email, password);
                if(user == null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> GetRegisterResult(string email)
        {
            try
            {
                if (email == null)
                {
                    return false;
                }
                User user = await _accountRepository.GetRegisterModel(email);
                if (user == null)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                if (id <= 0)
                    return false;
                await _accountRepository.Remove(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(UserUpdateCommand user)
        {
            try
            {
                if(user != null)
                {
                    await _accountRepository.Update(UserCommandConverter.UserUpdateCommandConvertToUserEntity(user));
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> GetIdByEmail(string email)
        {
            try
            {
                if (email == null)
                    return 0;
                User user = await _accountRepository.GetRegisterModel(email);
                if (user == null)
                    return 0;
                return user.Id;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<UserCommand> GetUserByEmail(string email)
        {
            try
            {
                if (email == null)
                    return null;
                User user = await _accountRepository.GetRegisterModel(email);
                if (user == null)
                    return null;
                return UserCommandConverter.UserEntityConvertToUserCommand(user);
            }
            catch
            {
                return null;
            }
        }

        public async Task<UserTokenCommand> GetUserTokenByLogin(string email)
        {
            try
            {
                User user = await _accountRepository.GetRegisterModel(email);
                UserTokenCommand command = UserCommandConverter.UserEntityConvertToUserToken(user);
                return command;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> SetUserToken(int id, string refreshToken)
        {
            try
            {
                User user = await _accountRepository.GetById(id);
                if (user == null)
                    return false;
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RefreshUserToken(int id, string refreshToken)
        {
            try
            {
                User user = await _accountRepository.GetById(id);
                if (user == null)
                    return false;
                user.RefreshToken = refreshToken;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> GetUserIdByLogin(string email)
        {
            try
            {
                User user = await _accountRepository.GetRegisterModel(email);
                return user.Id;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<string> GetUserNameById(int id)
        {
            try
            {
                User user = await _accountRepository.GetById(id);
                return user.Name;
            }
            catch
            {
                return null;
            }
        }
    }
}
