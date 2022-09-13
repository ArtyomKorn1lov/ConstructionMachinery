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
        Task<List<Advert>> GetAll(int count);
        Task<List<Advert>> GetAllWithoutUserId(int id, int count);
        Task<Advert> GetById(int id);
        Task Create(Advert advert);
        Task Update(Advert advert);
        Task Remove(int id);
        Task<List<Advert>> GetByName(string name, int count);
        Task<List<Advert>> GetByNameWithoutUserId(string name, int id, int count);
        Task<List<Advert>> GetByUserId(int id, int count);
        Task<Advert> GetForUpdate(int id);
        Task<int> GetLastAdvertId();
        Task<List<Advert>> GetUserAdvertsWithPendingConfirmationForCustomer(int id, int count);
        Task<List<Advert>> GetUserAdvertsWithPendingConfirmationForLandlord(int id, int count);
    }
}
