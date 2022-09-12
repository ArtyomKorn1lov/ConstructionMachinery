using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AdvertRepository : IAdvertRepository
    {
        private ConstructionMachineryDbContext _constructionMachineryDbContext;

        public AdvertRepository(ConstructionMachineryDbContext constructionMachineryDbContext)
        {
            _constructionMachineryDbContext = constructionMachineryDbContext;
        }

        public async Task Create(Advert advert)
        {
            await _constructionMachineryDbContext.Set<Advert>().AddAsync(advert);
        }

        public async Task<List<Advert>> GetAll()
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).ToListAsync();
        }

        public async Task<List<Advert>> GetAllWithoutUserId(int id)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Where(advert => advert.UserId != id).Include(advert => advert.Images).ToListAsync();
        }

        public async Task<Advert> GetById(int id)
        {
            return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.AvailableTimes).Include(advert => advert.Images).FirstOrDefaultAsync(advert => advert.Id == id);
        }

        public async Task<List<Advert>> GetByName(string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Where(advert => EF.Functions.Like(advert.Name, "%"+name+"%")).ToListAsync();
        }

        public async Task<List<Advert>> GetByNameWithoutUserId(string name, int id)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%") && advert.UserId != id).ToListAsync();
        }

        public async Task<List<Advert>> GetByUserId(int id)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Where(advert => advert.UserId == id).ToListAsync();
        }

        public async Task Remove(int id)
        {
            Advert advert = await GetById(id);
            if (advert != null)
                _constructionMachineryDbContext.Set<Advert>().Remove(advert);
        }

        public async Task Update(Advert advert)
        {
            Advert _advert = await GetById(advert.Id);
            _advert.CopyFrom(advert);
        }

        public async Task<Advert> GetForUpdate(int id)
        {
            return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.AvailableTimes).Include(advert => advert.Images).FirstOrDefaultAsync(advert => advert.Id == id);
        }

        public async Task<int> GetLastAdvertId()
        {
            return await _constructionMachineryDbContext.Set<Advert>().MaxAsync(advert => advert.Id);
        }

        public async Task<List<Advert>> GetUserAdvertsWithPendingConfirmationForCustomer(int id)
        {
            return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images)
                .Include(advert => advert.AvailableTimes)
                .Where(advert => advert.AvailableTimes.Any(time => time.AvailabilityRequestId != null 
                && _constructionMachineryDbContext.Set<AvailabilityRequest>().FirstOrDefault(request => request.Id == time.AvailabilityRequestId).UserId == id))
                .ToListAsync();
        }

        public async Task<List<Advert>> GetUserAdvertsWithPendingConfirmationForLandlord(int id)
        {
            return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images)
                .Include(advert => advert.AvailableTimes)
                .Where(advert => advert.AvailableTimes.Any(time => time.AvailabilityRequestId != null && time.AvailabilityStateId == 3))
                .Where(advert => advert.UserId == id).ToListAsync();
        }
    }
}
