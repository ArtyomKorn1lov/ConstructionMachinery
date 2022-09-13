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
    }
}
