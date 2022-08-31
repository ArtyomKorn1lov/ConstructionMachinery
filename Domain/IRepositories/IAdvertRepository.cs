using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IAdvertRepository
    {
        Task<List<Advert>> GetAll();
        Task<List<Advert>> GetAllWithoutUserId(int id);
        Task<Advert> GetById(int id);
        Task Create(Advert advert);
        Task Update(Advert advert);
        Task Remove(int id);
        Task<List<Advert>> GetByName(string name);
        Task<List<Advert>> GetByNameWithoutUserId(string name, int id);
        Task<List<Advert>> GetByUserId(int id);
        Task<Advert> GetForUpdate(int id);
        Task<int> GetLastAdvertId();
    }
}
