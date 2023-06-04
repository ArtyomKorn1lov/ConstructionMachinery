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
        Task<List<AdvertCommandList>> GetAll(FilterCommand filter, string name, string sort, int page);
        Task<List<AdvertCommandList>> GetAllWithoutUserId(FilterCommand filter, string name, string sort, int id, int page);
        Task<AdvertCommandInfo> GetById(int id);
        Task<AdvertCommandDetail> GetDetailAdvert(int id);
        Task<bool> Create(AdvertCommandCreate advert);
        Task<bool> Update(AdvertCommandUpdate advert);
        Task<bool> Remove(int id);
        Task<List<AdvertCommandList>> GetByName(string name, int page);
        Task<List<AdvertCommandList>> GetByNameWithoutUserId(string name, int id, int page);
        Task<List<AdvertCommandList>> GetByUserId(FilterCommand filter, string name, string sort, int id, int page);
        Task<AdvertCommandUpdate> GetForUpdate(int id, int userId);
        Task<int> GetUserIdByAdvert(int id);
        Task<int> GetLastAdvertId();
        Task<List<AdvertCommandForRequest>> GetForRequestCustomer(int id, int page);
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
