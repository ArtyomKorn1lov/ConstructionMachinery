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
            throw new NotImplementedException();
        }

        public async Task<bool> Create(UserCreateCommand user)
        {
            try
            {
                if(user != null)
                {
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
    }
}
