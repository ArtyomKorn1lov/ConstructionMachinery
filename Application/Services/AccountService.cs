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
                if (user == null)
                    return false;
                if (user.Password.Trim() == "" || user.Password == null)
                    return false;
                if (user.Name.Trim() == "" || user.Name == null)
                    return false;
                if (user.Email.Trim() == "" || user.Email == null)
                    return false;
                if (user.Phone.Trim() == "" || user.Phone == null)
                    return false;
                if (user.Address.Trim() == "" || user.Address == null)
                    return false;
                user.Created = DateTime.Now;
                await _accountRepository.Create(UserCommandConverter.UserCreateCommandConvertToUserEntity(user));
                return true;
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
                if (id <= 0)
                    return null;
                UserCommand user = UserCommandConverter.UserEntityConvertToUserCommand(await _accountRepository.GetById(id));
                if (user == null)
                    return null;
                if (id != user.Id)
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
                if (email == null || password == null
                    || email.Trim() == "" || password.Trim() == "")
                    return false;
                User user = await _accountRepository.GetLoginModel(email, password);
                if (user == null)
                    return false;
                if (email != user.Email || password != user.Password)
                    return false;
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
                if (email == null || email.Trim() == "")
                    return false;
                User user = await _accountRepository.GetRegisterModel(email);
                if (user != null)
                    return false;
                return true;
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
                User currentUser = await _accountRepository.GetById(id);
                if (currentUser == null)
                    return false;
                if (currentUser.Id != id)
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
                if (user == null || user.Id <= 0)
                    return false;
                User currentUser = await _accountRepository.GetById(user.Id);
                if (currentUser == null)
                    return false;
                if (currentUser.Id != user.Id)
                    return false;
                if (user.Password.Trim() == "" || user.Password == null)
                    return false;
                if (user.Name.Trim() == "" || user.Name == null)
                    return false;
                if (user.Email.Trim() == "" || user.Email == null)
                    return false;
                if (user.Phone.Trim() == "" || user.Phone == null)
                    return false;
                if (user.Address.Trim() == "" || user.Address == null)
                    return false;
                await _accountRepository.Update(UserCommandConverter.UserUpdateCommandConvertToUserEntity(user));
                return true;
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
                if (email == null || email.Trim() == "")
                    return 0;
                User user = await _accountRepository.GetRegisterModel(email);
                if (user == null)
                    return 0;
                if (user.Email != email)
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
                if (email == null || email.Trim() == "")
                    return null;
                User user = await _accountRepository.GetRegisterModel(email);
                if (user == null)
                    return null;
                if (user.Email != email)
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
                if (email == null || email.Trim() == "")
                    return null;
                User user = await _accountRepository.GetRegisterModel(email);
                if (user == null)
                    return null;
                if (user.Email != email)
                    return null;
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
                if (id <= 0 || refreshToken == null || refreshToken.Trim() == "")
                    return false;
                User user = await _accountRepository.GetById(id);
                if (user == null)
                    return false;
                if (id != user.Id)
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
                if (id <= 0 || refreshToken == null || refreshToken.Trim() == "")
                    return false;
                User user = await _accountRepository.GetById(id);
                if (user == null)
                    return false;
                if (id != user.Id)
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
                if (email == null || email.Trim() == "")
                    return 0;
                User user = await _accountRepository.GetRegisterModel(email);
                if (user == null)
                    return 0;
                if (user.Email != email)
                    return 0;
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
                if (id <= 0)
                    return null;
                User user = await _accountRepository.GetById(id);
                if (user == null)
                    return null;
                if (user.Id != id)
                    return null;
                return user.Name;
            }
            catch
            {
                return null;
            }
        }
    }
}
