using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;

namespace Application.IServices
{
    public interface IAdvertService
    {
        Task<List<AdvertCommandList>> GetAll(int count);
        Task<List<AdvertCommandList>> GetAllWithoutUserId(int id, int count);
        Task<AdvertCommandInfo> GetById(int id);
        Task<bool> Create(AdvertCommandCreate advert);
        Task<bool> Update(AdvertCommandUpdate advert);
        Task<bool> Remove(int id);
        Task<List<AdvertCommandList>> GetByName(string name, int count);
        Task<List<AdvertCommandList>> GetByNameWithoutUserId(string name, int id, int count);
        Task<List<AdvertCommandList>> GetByUserId(int id, int count);
        Task<AdvertCommandUpdate> GetForUpdate(int id);
        Task<int> GetUserIdByAdvert(int id);
        Task<int> GetLastAdvertId();
        Task<List<AdvertCommandForRequest>> GetForRequestCustomer(int id, int count);
        Task<List<AdvertCommandForRequest>> GetForRequestLandlord(int id, int count);
        Task<List<AdvertCommandList>> GetSortByPriceMax(int count);
        Task<List<AdvertCommandList>> GetSortByPriceMin(int count);
        Task<List<AdvertCommandList>> GetSortByPriceMaxWithoutUserId(int count, int id);
        Task<List<AdvertCommandList>> GetSortByPriceMinWithoutUserId(int count, int id);
        Task<List<AdvertCommandList>> GetSortByRatingMax(int count);
        Task<List<AdvertCommandList>> GetSortByRatingMin(int count);
        Task<List<AdvertCommandList>> GetSortByRatingMaxWithoutUserId(int count, int id);
        Task<List<AdvertCommandList>> GetSortByRatingMinWithoutUserId(int count, int id);
        Task<List<AdvertCommandList>> GetSortByDateMin(int count);
        Task<List<AdvertCommandList>> GetSortByDateMinWithoutUserId(int count, int id);
        Task<List<AdvertCommandList>> GetSortByPriceMaxByName(int count, string name);
        Task<List<AdvertCommandList>> GetSortByPriceMinByName(int count, string name);
        Task<List<AdvertCommandList>> GetSortByPriceMaxWithoutUserIdByName(int count, int id, string name);
        Task<List<AdvertCommandList>> GetSortByPriceMinWithoutUserIdByName(int count, int id, string name);
        Task<List<AdvertCommandList>> GetSortByRatingMaxByName(int count, string name);
        Task<List<AdvertCommandList>> GetSortByRatingMinByName(int count, string name);
        Task<List<AdvertCommandList>> GetSortByRatingMaxWithoutUserIdByName(int count, int id, string name);
        Task<List<AdvertCommandList>> GetSortByRatingMinWithoutUserIdByName(int count, int id, string name);
        Task<List<AdvertCommandList>> GetSortByDateMinByName(int count, string name);
        Task<List<AdvertCommandList>> GetSortByDateMinWithoutUserIdByName(int count, int id, string name);
    }
}
