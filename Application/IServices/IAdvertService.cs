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
        Task<List<AdvertCommandList>> GetByUserId(FilterCommand filter, string name, string sort, int id, int page);
        Task<AdvertCommandUpdate> GetForUpdate(int id, int userId);
        Task<int> GetUserIdByAdvert(int id);
        Task<int> GetLastAdvertId();
        Task<List<AdvertCommandForRequest>> GetForRequestCustomer(int id, int page);
        Task<List<AdvertCommandForRequest>> GetForRequestLandlord(int id, int page);
    }
}
