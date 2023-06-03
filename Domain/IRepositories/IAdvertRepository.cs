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
        Task<List<Advert>> GetAll(Filter filter, string name, int page);
        Task<List<Advert>> GetAllWithoutUserId(Filter filter, string name, int id, int page);
        Task<Advert> GetById(int id);
        Task<Advert> GetByIdForUpdate(int id);
        Task Create(Advert advert);
        Task Update(Advert advert);
        Task Remove(int id);
        Task<Advert> GetAdvertByTimeId(int id);
        Task<List<Advert>> GetByName(string name, int page);
        Task<List<Advert>> GetByNameWithoutUserId(string name, int id, int page);
        Task<List<Advert>> GetByUserId(Filter filter, string name, int id, int page);
        Task<List<Advert>> GetSortByPriceMaxByUserId(Filter filter, string name, int id, int page);
        Task<List<Advert>> GetSortByPriceMinByUserId(Filter filter, string name, int id, int page);
        Task<List<Advert>> GetSortByRatingMaxByUserId(Filter filter, string name, int id, int page);
        Task<List<Advert>> GetSortByRatingMinByUserId(Filter filter, string name, int id, int page);
        Task<List<Advert>> GetSortByDateMinByUserId(Filter filter, string name, int id, int page);
        Task<Advert> GetForUpdate(int id);
        Task<int> GetLastAdvertId();
        Task<List<Advert>> GetUserAdvertsWithPendingConfirmationForCustomer(int id, int page);
        Task<List<Advert>> GetSortByPriceMax(Filter filter, string name, int page);
        Task<List<Advert>> GetSortByPriceMin(Filter filter, string name, int page);
        Task<List<Advert>> GetSortByPriceMaxWithoutUserId(Filter filter, string name, int page, int id);
        Task<List<Advert>> GetSortByPriceMinWithoutUserId(Filter filter, string name, int page, int id);
        Task<List<Advert>> GetSortByRatingMax(Filter filter, string name, int page);
        Task<List<Advert>> GetSortByRatingMin(Filter filter, string name, int page);
        Task<List<Advert>> GetSortByRatingMaxWithoutUserId(Filter filter, string name, int page, int id);
        Task<List<Advert>> GetSortByRatingMinWithoutUserId(Filter filter, string name, int page, int id);
        Task<List<Advert>> GetSortByDateMin(Filter filter, string name, int page);
        Task<List<Advert>> GetSortByDateMinWithoutUserId(Filter filter, string name, int page, int id);
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
