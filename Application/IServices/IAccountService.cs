using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;

namespace Application.IServices
{
    public interface IAccountService
    {
        string HashPassword(string password);
        Task<bool> GetLoginResult(string email, string password);
        Task<bool> GetRegisterResult(string email);
        Task<bool> Create(UserCreateCommand user);
        Task<bool> Update(UserUpdateCommand user);
        Task<bool> Remove(int id);
        Task<UserCommand> GetById(int id);
        Task<int> GetIdByEmail(string email);
        Task<UserCommand> GetUserByEmail(string email);
        Task<UserTokenCommand> GetUserTokenByLogin(string email);
        Task<bool> SetUserToken(int id, string refreshToken);
        Task<bool> RefreshUserToken(int id, string refreshToken);
        Task<int> GetUserIdByLogin(string email);
        Task<string> GetUserNameById(int id);
    }
}
