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
        Task<Advert> GetByIdForUpdate(int id);
        Task Create(Advert advert);
        Task Update(Advert advert);
        Task Remove(int id);
        Task<List<Advert>> GetByName(string name, int count);
        Task<List<Advert>> GetByNameWithoutUserId(string name, int id, int count);
        Task<List<Advert>> GetByUserId(int id, int count);
        Task<List<Advert>> GetSortByPriceMaxByUserId(int id, int count);
        Task<List<Advert>> GetSortByPriceMinByUserId(int id, int count);
        Task<List<Advert>> GetSortByRatingMaxByUserId(int id, int count);
        Task<List<Advert>> GetSortByRatingMinByUserId(int id, int count);
        Task<List<Advert>> GetSortByDateMinByUserId(int id, int count);
        Task<Advert> GetForUpdate(int id);
        Task<int> GetLastAdvertId();
        Task<List<Advert>> GetUserAdvertsWithPendingConfirmationForCustomer(int id, int count);
        Task<List<Advert>> GetSortByPriceMax(int count);
        Task<List<Advert>> GetSortByPriceMin(int count);
        Task<List<Advert>> GetSortByPriceMaxWithoutUserId(int count, int id);
        Task<List<Advert>> GetSortByPriceMinWithoutUserId(int count, int id);
        Task<List<Advert>> GetSortByRatingMax(int count);
        Task<List<Advert>> GetSortByRatingMin(int count);
        Task<List<Advert>> GetSortByRatingMaxWithoutUserId(int count, int id);
        Task<List<Advert>> GetSortByRatingMinWithoutUserId(int count, int id);
        Task<List<Advert>> GetSortByDateMin(int count);
        Task<List<Advert>> GetSortByDateMinWithoutUserId(int count, int id);
        Task<List<Advert>> GetSortByPriceMaxByName(int count, string name);
        Task<List<Advert>> GetSortByPriceMinByName(int count, string name);
        Task<List<Advert>> GetSortByPriceMaxWithoutUserIdByName(int count, int id, string name);
        Task<List<Advert>> GetSortByPriceMinWithoutUserIdByName(int count, int id, string name);
        Task<List<Advert>> GetSortByRatingMaxByName(int count, string name);
        Task<List<Advert>> GetSortByRatingMinByName(int count, string name);
        Task<List<Advert>> GetSortByRatingMaxWithoutUserIdByName(int count, int id, string name);
        Task<List<Advert>> GetSortByRatingMinWithoutUserIdByName(int count, int id, string name);
        Task<List<Advert>> GetSortByDateMinByName(int count, string name);
        Task<List<Advert>> GetSortByDateMinWithoutUserIdByName(int count, int id, string name);
    }
}
