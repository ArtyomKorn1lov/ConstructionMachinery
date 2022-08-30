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
        Task<List<AdvertCommandList>> GetAll();
        Task<List<AdvertCommandList>> GetAllWithoutUserId(int id);
        Task<AdvertCommandInfo> GetById(int id);
        Task<bool> Create(AdvertCommandCreate advert);
        Task<bool> Update(AdvertCommandUpdate advert);
        Task<bool> Remove(int id);
        Task<List<AdvertCommandList>> GetByName(string name);
        Task<List<AdvertCommandList>> GetByNameWithoutUserId(string name, int id);
        Task<List<AdvertCommandList>> GetByUserId(int id);
        Task<AdvertCommandUpdate> GetForUpdate(int id);
        Task<int> GetUserIdByAdvert(int id);
    }
}
