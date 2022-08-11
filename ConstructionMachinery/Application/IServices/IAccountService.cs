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
        Task<bool> GetLoginResult(string email, string password);
        Task<bool> GetRegisterResult(string email);
        Task<bool> Create(UserCreateCommand user);
        Task<bool> Update(UserUpdateCommand user);
        Task<bool> Remove(int id);
        Task<UserCommand> GetById(int id);
    }
}
