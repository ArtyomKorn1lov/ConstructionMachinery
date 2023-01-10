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

        public async Task<List<Advert>> GetAll(int count)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.EditDate).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetAllWithoutUserId(int id, int count)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Where(advert => advert.UserId != id).Include(advert => advert.Reviews)
                .Include(advert => advert.Images).OrderByDescending(advert => advert.EditDate).Take(count).ToListAsync();
        }

        public async Task<Advert> GetById(int id)
        {
            return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.AvailableTimes.Where(time => time.Date >= DateTime.Now))
                .Include(advert => advert.Images)
                .FirstOrDefaultAsync(advert => advert.Id == id);
        }

        public async Task<Advert> GetByIdForUpdate(int id)
        {
            return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.AvailableTimes)
                .Include(advert => advert.Images)
                .FirstOrDefaultAsync(advert => advert.Id == id);
        }

        public async Task<List<Advert>> GetByName(string name, int count)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%")).OrderByDescending(advert => advert.EditDate)
                .Include(advert => advert.Reviews).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetByNameWithoutUserId(string name, int id, int count)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%") && advert.UserId != id)
                .Include(advert => advert.Reviews).OrderByDescending(advert => advert.EditDate).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetByUserId(int id, int count)
        {
            return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images).Include(advert => advert.Reviews).OrderByDescending(advert => advert.EditDate)
                .Where(advert => advert.UserId == id).Take(count).ToListAsync();
        }

        public async Task Remove(int id)
        {
            Advert advert = await GetByIdForUpdate(id);
            if (advert != null)
                _constructionMachineryDbContext.Set<Advert>().Remove(advert);
        }

        public async Task Update(Advert advert)
        {
            Advert _advert = await GetByIdForUpdate(advert.Id);
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

        public async Task<List<Advert>> GetUserAdvertsWithPendingConfirmationForCustomer(int id, int count)
        {
            return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images)
                .Include(advert => advert.AvailableTimes)
                .Where(advert => advert.AvailableTimes.Any(time => time.AvailabilityRequestId != null 
                && _constructionMachineryDbContext.Set<AvailabilityRequest>().FirstOrDefault(request => request.Id == time.AvailabilityRequestId).UserId == id))
                .OrderBy(advert => advert.EditDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Advert>> GetUserAdvertsWithPendingConfirmationForLandlord(int id, int count)
        {
            return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images)
                .Include(advert => advert.AvailableTimes)
                .Where(advert => advert.AvailableTimes.Any(time => time.AvailabilityRequestId != null && time.AvailabilityStateId == 3))
                .Where(advert => advert.UserId == id)
                .OrderBy(advert => advert.EditDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMax(int count)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Price).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMin(int count)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Price).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMaxWithoutUserId(int count, int id)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Price).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMinWithoutUserId(int count, int id)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Price).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByDateMin(int count)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.EditDate).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByDateMinWithoutUserId(int count, int id)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.EditDate).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMax(int count)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId)).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMin(int count)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId)).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMaxWithoutUserId(int count, int id)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMinWithoutUserId(int count, int id)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMaxByName(int count, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .OrderBy(advert => advert.Price).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMinByName(int count, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .OrderByDescending(advert => advert.Price).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMaxWithoutUserIdByName(int count, int id, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Price).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMinWithoutUserIdByName(int count, int id, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Price).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMaxByName(int count, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId)).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMinByName(int count, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId)).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMaxWithoutUserIdByName(int count, int id, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMinWithoutUserIdByName(int count, int id, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByDateMinByName(int count, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .OrderBy(advert => advert.EditDate).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByDateMinWithoutUserIdByName(int count, int id, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.EditDate).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMaxByUserId(int id, int count)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Price).Where(advert => advert.UserId == id).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMinByUserId(int id, int count)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Price).Where(advert => advert.UserId == id).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMaxByUserId(int id, int count)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.UserId == id).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMinByUserId(int id, int count)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.UserId == id).Take(count).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByDateMinByUserId(int id, int count)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.EditDate).Where(advert => advert.UserId == id).Take(count).ToListAsync();
        }
    }
}
