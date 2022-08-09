using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IAccountRepository
    {
        Task<User> GetLoginModel(string login, string password);
        Task<User> GetRegisterModel(string login);
        Task Create(User user);
        Task Update(User user);
        Task Remove(int id);
        Task<User> GetById(int id);
    }
}
