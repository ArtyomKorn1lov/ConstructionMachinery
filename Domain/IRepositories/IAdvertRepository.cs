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
        Task<List<Advert>> GetAll(int page);
        Task<List<Advert>> GetAllWithoutUserId(int id, int page);
        Task<Advert> GetById(int id);
        Task<Advert> GetByIdForUpdate(int id);
        Task Create(Advert advert);
        Task Update(Advert advert);
        Task Remove(int id);
        Task<List<Advert>> GetByName(string name, int page);
        Task<List<Advert>> GetByNameWithoutUserId(string name, int id, int page);
        Task<List<Advert>> GetByUserId(int id, int page);
        Task<List<Advert>> GetSortByPriceMaxByUserId(int id, int page);
        Task<List<Advert>> GetSortByPriceMinByUserId(int id, int page);
        Task<List<Advert>> GetSortByRatingMaxByUserId(int id, int page);
        Task<List<Advert>> GetSortByRatingMinByUserId(int id, int page);
        Task<List<Advert>> GetSortByDateMinByUserId(int id, int page);
        Task<Advert> GetForUpdate(int id);
        Task<int> GetLastAdvertId();
        Task<List<Advert>> GetUserAdvertsWithPendingConfirmationForCustomer(int id, int page);
        Task<List<Advert>> GetSortByPriceMax(int page);
        Task<List<Advert>> GetSortByPriceMin(int page);
        Task<List<Advert>> GetSortByPriceMaxWithoutUserId(int page, int id);
        Task<List<Advert>> GetSortByPriceMinWithoutUserId(int page, int id);
        Task<List<Advert>> GetSortByRatingMax(int page);
        Task<List<Advert>> GetSortByRatingMin(int page);
        Task<List<Advert>> GetSortByRatingMaxWithoutUserId(int page, int id);
        Task<List<Advert>> GetSortByRatingMinWithoutUserId(int page, int id);
        Task<List<Advert>> GetSortByDateMin(int page);
        Task<List<Advert>> GetSortByDateMinWithoutUserId(int page, int id);
        Task<List<Advert>> GetSortByPriceMaxByName(int page, string name);
        Task<List<Advert>> GetSortByPriceMinByName(int page, string name);
        Task<List<Advert>> GetSortByPriceMaxWithoutUserIdByName(int page, int id, string name);
        Task<List<Advert>> GetSortByPriceMinWithoutUserIdByName(int page, int id, string name);
        Task<List<Advert>> GetSortByRatingMaxByName(int page, string name);
        Task<List<Advert>> GetSortByRatingMinByName(int page, string name);
        Task<List<Advert>> GetSortByRatingMaxWithoutUserIdByName(int page, int id, string name);
        Task<List<Advert>> GetSortByRatingMinWithoutUserIdByName(int page, int id, string name);
        Task<List<Advert>> GetSortByDateMinByName(int page, string name);
        Task<List<Advert>> GetSortByDateMinWithoutUserIdByName(int page, int id, string name);
    }
}
