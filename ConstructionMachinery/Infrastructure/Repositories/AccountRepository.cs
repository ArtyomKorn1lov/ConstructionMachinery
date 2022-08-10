using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private ConstructionMachineryDbContext _constructionMachineryDbContext;

        public AccountRepository(ConstructionMachineryDbContext constructionMachineryDbContext)
        {
            _constructionMachineryDbContext = constructionMachineryDbContext;
        }

        public async Task<User> GetLoginModel(string email, string password)
        {
            return await _constructionMachineryDbContext.Set<User>().FirstOrDefaultAsync(user => user.Email == email && user.Password == password);
        }

        public async Task<User> GetRegisterModel(string email)
        {
            return await _constructionMachineryDbContext.Set<User>().FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task Create(User user)
        {
            await _constructionMachineryDbContext.Set<User>().AddAsync(user);
        }

        public async Task<User> GetById(int id)
        {
            return await _constructionMachineryDbContext.Set<User>().FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task Remove(int id)
        {
            User user = await GetById(id);
            if (user != null)
                _constructionMachineryDbContext.Set<User>().Remove(user);
        }

        public async Task Update(User user)
        {
            User _user = await GetById(user.Id);
            _user.CopyFrom(user);
        }
    }
}
