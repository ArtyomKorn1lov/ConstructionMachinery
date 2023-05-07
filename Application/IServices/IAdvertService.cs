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
        List<AvailiableDayCommand> PackageToDayCommands(List<AvailableTimeCommand> availableTimeCommands);
        List<AvailiableDayCommand> SortDateCommmands(List<AvailiableDayCommand> availiableDayCommands);
        Task<List<AdvertCommandList>> GetAll(int page);
        Task<List<AdvertCommandList>> GetAllWithoutUserId(int id, int page);
        Task<AdvertCommandInfo> GetById(int id);
        Task<AdvertCommandDetail> GetDetailAdvert(int id);
        Task<bool> Create(AdvertCommandCreate advert);
        Task<bool> Update(AdvertCommandUpdate advert);
        Task<bool> Remove(int id);
        Task<List<AdvertCommandList>> GetByName(string name, int page);
        Task<List<AdvertCommandList>> GetByNameWithoutUserId(string name, int id, int page);
        Task<List<AdvertCommandList>> GetByUserId(int id, int page);
        Task<List<AdvertCommandList>> GetSortByPriceMaxByUserId(int id, int page);
        Task<List<AdvertCommandList>> GetSortByPriceMinByUserId(int id, int page);
        Task<List<AdvertCommandList>> GetSortByRatingMaxByUserId(int id, int page);
        Task<List<AdvertCommandList>> GetSortByRatingMinByUserId(int id, int page);
        Task<List<AdvertCommandList>> GetSortByDateMinByUserId(int id, int page);
        Task<AdvertCommandUpdate> GetForUpdate(int id, int userId);
        Task<int> GetUserIdByAdvert(int id);
        Task<int> GetLastAdvertId();
        Task<List<AdvertCommandForRequest>> GetForRequestCustomer(int id, int page);
        Task<List<AdvertCommandList>> GetSortByPriceMax(int page);
        Task<List<AdvertCommandList>> GetSortByPriceMin(int page);
        Task<List<AdvertCommandList>> GetSortByPriceMaxWithoutUserId(int page, int id);
        Task<List<AdvertCommandList>> GetSortByPriceMinWithoutUserId(int page, int id);
        Task<List<AdvertCommandList>> GetSortByRatingMax(int page);
        Task<List<AdvertCommandList>> GetSortByRatingMin(int page);
        Task<List<AdvertCommandList>> GetSortByRatingMaxWithoutUserId(int page, int id);
        Task<List<AdvertCommandList>> GetSortByRatingMinWithoutUserId(int page, int id);
        Task<List<AdvertCommandList>> GetSortByDateMin(int page);
        Task<List<AdvertCommandList>> GetSortByDateMinWithoutUserId(int page, int id);
        Task<List<AdvertCommandList>> GetSortByPriceMaxByName(int page, string name);
        Task<List<AdvertCommandList>> GetSortByPriceMinByName(int page, string name);
        Task<List<AdvertCommandList>> GetSortByPriceMaxWithoutUserIdByName(int page, int id, string name);
        Task<List<AdvertCommandList>> GetSortByPriceMinWithoutUserIdByName(int page, int id, string name);
        Task<List<AdvertCommandList>> GetSortByRatingMaxByName(int page, string name);
        Task<List<AdvertCommandList>> GetSortByRatingMinByName(int page, string name);
        Task<List<AdvertCommandList>> GetSortByRatingMaxWithoutUserIdByName(int page, int id, string name);
        Task<List<AdvertCommandList>> GetSortByRatingMinWithoutUserIdByName(int page, int id, string name);
        Task<List<AdvertCommandList>> GetSortByDateMinByName(int page, string name);
        Task<List<AdvertCommandList>> GetSortByDateMinWithoutUserIdByName(int page, int id, string name);
    }
}
