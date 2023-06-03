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
        private const int TAKE_COUNT = 10;

        public AdvertRepository(ConstructionMachineryDbContext constructionMachineryDbContext)
        {
            _constructionMachineryDbContext = constructionMachineryDbContext;
        }

        public async Task Create(Advert advert)
        {
            await _constructionMachineryDbContext.Set<Advert>().AddAsync(advert);
        }

        public async Task<List<Advert>> GetAll(Filter filter, string name, int page)
        {
            //1-0001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //2-0010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //3-0100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //4-1000
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //5-0011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //6-0101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //7-1001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //8-0110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //9-1010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //10-1100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //11-0111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //12-1011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //13-1101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //14-1110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //15-1111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //16-0000
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.EditDate).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetAllWithoutUserId(Filter filter, string name, int id, int page)
        {
            //1-0001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Where(advert => advert.UserId != id).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Include(advert => advert.Images).OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //2-0010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Where(advert => advert.UserId != id).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Include(advert => advert.Images).OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //3-0100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Where(advert => advert.UserId != id).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Include(advert => advert.Images).OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //4-1000
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Where(advert => advert.UserId != id).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Include(advert => advert.Images).OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //5-0011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Where(advert => advert.UserId != id).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Include(advert => advert.Images).OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //6-0101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Where(advert => advert.UserId != id).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Include(advert => advert.Images).OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //7-1001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Where(advert => advert.UserId != id).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Include(advert => advert.Images).OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //8-0110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Where(advert => advert.UserId != id).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Include(advert => advert.Images).OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //9-1010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Where(advert => advert.UserId != id).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Include(advert => advert.Images).OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //10-1100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Where(advert => advert.UserId != id).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Include(advert => advert.Images).OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //11-0111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Where(advert => advert.UserId != id).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Include(advert => advert.Images).OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //12-1011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Where(advert => advert.UserId != id).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Include(advert => advert.Images).OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //13-1101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Where(advert => advert.UserId != id).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Include(advert => advert.Images).OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //14-1110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Where(advert => advert.UserId != id).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Include(advert => advert.Images).OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //15-1111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Where(advert => advert.UserId != id).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Include(advert => advert.Images).OrderByDescending(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //16-0000
            return await _constructionMachineryDbContext.Set<Advert>().Where(advert => advert.UserId != id).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Include(advert => advert.Images).OrderByDescending(advert => advert.EditDate).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
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

        public async Task<List<Advert>> GetByName(string name, int page)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%")).OrderByDescending(advert => advert.EditDate)
                .Include(advert => advert.Reviews).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetByNameWithoutUserId(string name, int id, int page)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%") && advert.UserId != id)
                .Include(advert => advert.Reviews).OrderByDescending(advert => advert.EditDate).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetByUserId(Filter filter, string name, int id, int page)
        {
            //1-0001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images).Include(advert => advert.Reviews).OrderByDescending(advert => advert.EditDate)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //2-0010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images).Include(advert => advert.Reviews).OrderByDescending(advert => advert.EditDate)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //3-0100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images).Include(advert => advert.Reviews).OrderByDescending(advert => advert.EditDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //4-1000
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images).Include(advert => advert.Reviews).OrderByDescending(advert => advert.EditDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //5-0011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images).Include(advert => advert.Reviews).OrderByDescending(advert => advert.EditDate)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //6-0101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images).Include(advert => advert.Reviews).OrderByDescending(advert => advert.EditDate)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //7-1001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images).Include(advert => advert.Reviews).OrderByDescending(advert => advert.EditDate)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //8-0110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images).Include(advert => advert.Reviews).OrderByDescending(advert => advert.EditDate)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //9-1010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images).Include(advert => advert.Reviews).OrderByDescending(advert => advert.EditDate)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //10-1100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images).Include(advert => advert.Reviews).OrderByDescending(advert => advert.EditDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //11-0111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images).Include(advert => advert.Reviews).OrderByDescending(advert => advert.EditDate)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //12-1011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images).Include(advert => advert.Reviews).OrderByDescending(advert => advert.EditDate)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //13-1101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images).Include(advert => advert.Reviews).OrderByDescending(advert => advert.EditDate)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //14-1110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images).Include(advert => advert.Reviews).OrderByDescending(advert => advert.EditDate)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //15-1111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images).Include(advert => advert.Reviews).OrderByDescending(advert => advert.EditDate)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //16-0000
            return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images).Include(advert => advert.Reviews).OrderByDescending(advert => advert.EditDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
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

        public async Task<Advert> GetAdvertByTimeId(int id)
        {
            return await _constructionMachineryDbContext.Set<Advert>().FirstOrDefaultAsync(advert => advert.AvailableTimes.Any(time => time.Id == id));
        }

        public async Task<List<Advert>> GetUserAdvertsWithPendingConfirmationForCustomer(int id, int page)
        {
            return await _constructionMachineryDbContext.Set<Advert>()
                .Include(advert => advert.Images)
                .Include(advert => advert.AvailableTimes)
                .Where(advert => advert.AvailableTimes.Any(time => time.AvailabilityRequestId != null 
                && _constructionMachineryDbContext.Set<AvailabilityRequest>().FirstOrDefault(request => request.Id == time.AvailabilityRequestId).UserId == id))
                .OrderBy(advert => advert.EditDate)
                .Skip(page*TAKE_COUNT).Take(TAKE_COUNT)
                .ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMax(Filter filter, string name, int page)
        {
            //1-0001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //2-0010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //3-0100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //4-1000
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //5-0011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //6-0101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //7-1001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //8-0110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //9-1010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //10-1100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //11-0111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //12-1011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //13-1101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //14-1110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //15-1111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //16-0000
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMin(Filter filter, string name, int page)
        {
            //1-0001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //2-0010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //3-0100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //4-1000
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //5-0011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //6-0101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //7-1001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //8-0110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //9-1010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //10-1100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //11-0111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //12-1011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //13-1101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //14-1110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //15-1111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //16-0000
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMaxWithoutUserId(Filter filter, string name, int page, int id)
        {
            //1-0001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //2-0010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //3-0100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //4-1000
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //5-0011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //6-0101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //7-1001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //8-0110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //9-1010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //10-1100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //11-0111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //12-1011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //13-1101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //14-1110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //15-1111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //16-0000
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Price).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMinWithoutUserId(Filter filter, string name, int page, int id)
        {
            //1-0001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //2-0010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //3-0100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //4-1000
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //5-0011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //6-0101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //7-1001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //8-0110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //9-1010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //10-1100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //11-0111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //12-1011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //13-1101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //14-1110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //15-1111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Price).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //16-0000
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Price).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByDateMin(Filter filter, string name, int page)
        {
            //1-0001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //2-0010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //3-0100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //4-1000
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //5-0011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //6-0101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //7-1001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //8-0110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //9-1010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //10-1100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //11-0111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //12-1011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //13-1101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //14-1110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //15-1111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //16-0000
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.EditDate).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByDateMinWithoutUserId(Filter filter, string name, int page, int id)
        {
            //1-0001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //2-0010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //3-0100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //4-1000
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //5-0011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //6-0101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //7-1001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //8-0110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //9-1010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //10-1100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //11-0111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //12-1011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //13-1101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //14-1110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //15-1111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.EditDate).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //16-0000
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.EditDate).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMax(Filter filter, string name, int page)
        {
            //1-0001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //2-0010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //3-0100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //4-1000
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //5-0011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //6-0101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //7-1001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //8-0110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //9-1010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //10-1100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //11-0111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //12-1011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //13-1101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //14-1110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //15-1111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //16-0000
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMin(Filter filter, string name, int page)
        {
            //1-0001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //2-0010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //3-0100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //4-1000
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //5-0011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //6-0101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //7-1001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //8-0110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //9-1010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //10-1100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //11-0111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //12-1011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //13-1101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //14-1110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //15-1111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //16-0000
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMaxWithoutUserId(Filter filter, string name, int page, int id)
        {
            //1-0001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //2-0010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //3-0100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //4-1000
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //5-0011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //6-0101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //7-1001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //8-0110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //9-1010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //10-1100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //11-0111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //12-1011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //13-1101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //14-1110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //15-1111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //16-0000
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMinWithoutUserId(Filter filter, string name, int page, int id)
        {
            //1-0001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //2-0010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //3-0100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //4-1000
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //5-0011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //6-0101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //7-1001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //8-0110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //9-1010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //10-1100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //11-0111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //12-1011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //13-1101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //14-1110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //15-1111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //16-0000
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMaxByName(int page, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .OrderBy(advert => advert.Price).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMinByName(int page, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .OrderByDescending(advert => advert.Price).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMaxWithoutUserIdByName(int page, int id, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Price).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMinWithoutUserIdByName(int page, int id, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Price).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMaxByName(int page, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMinByName(int page, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId)).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMaxWithoutUserIdByName(int page, int id, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => advert.UserId != id).OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMinWithoutUserIdByName(int page, int id, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByDateMinByName(int page, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .OrderBy(advert => advert.EditDate).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByDateMinWithoutUserIdByName(int page, int id, string name)
        {
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => advert.UserId != id).OrderBy(advert => advert.EditDate).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMaxByUserId(Filter filter, string name, int id, int page)
        {
            //1-0001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //2-0010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //3-0100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //4-1000
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //5-0011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //6-0101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //7-1001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //8-0110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //9-1010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //10-1100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //11-0111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //12-1011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //13-1101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //14-1110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //15-1111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //16-0000
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByPriceMinByUserId(Filter filter, string name, int id, int page)
        {
            //1-0001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //2-0010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //3-0100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //4-1000
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //5-0011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //6-0101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //7-1001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //8-0110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //9-1010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //10-1100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //11-0111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //12-1011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //13-1101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //14-1110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //15-1111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //16-0000
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderByDescending(advert => advert.Price).Where(advert => advert.UserId == id).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMaxByUserId(Filter filter, string name, int id, int page)
        {
            //1-0001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //2-0010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //3-0100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //4-1000
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //5-0011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //6-0101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //7-1001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //8-0110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //9-1010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //10-1100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //11-0111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //12-1011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //13-1101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //14-1110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //15-1111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //16-0000
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderByDescending(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByRatingMinByUserId(Filter filter, string name, int id, int page)
        {
            //1-0001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //2-0010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //3-0100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //4-1000
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //5-0011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //6-0101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //7-1001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //8-0110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //9-1010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //10-1100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //11-0111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //12-1011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //13-1101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //14-1110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //15-1111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //16-0000
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .OrderBy(advert => advert.Reviews.Average(review => review.ReviewStateId))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .Where(advert => advert.UserId == id).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<List<Advert>> GetSortByDateMinByUserId(Filter filter, string name, int id, int page)
        {
            //1-0001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //2-0010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //3-0100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //4-1000
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //5-0011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //6-0101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //7-1001
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //8-0110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //9-1010
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //10-1100
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //11-0111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime == 0 || filter.EndTime == 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //12-1011
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //13-1101
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate == DateTime.MinValue || filter.EndPublishDate == DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //14-1110
            if ((filter.StartPrice == 0 || filter.EndPrice == 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //15-1111
            if ((filter.StartPrice != 0 && filter.EndPrice != 0)
                || (filter.StartPublishDate != DateTime.MinValue && filter.EndPublishDate != DateTime.MinValue)
                || (filter.StartDate != DateTime.MinValue && filter.EndDate != DateTime.MinValue)
                || (filter.StartTime != 0 && filter.EndTime != 0))
            {
                return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => advert.Price >= filter.StartPrice && advert.Price <= filter.EndPrice)
                .Where(advert => advert.EditDate >= filter.StartPublishDate && advert.EditDate <= filter.EndPublishDate)
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date >= filter.StartDate && time.Date <= filter.EndDate) && time.AvailabilityStateId == 1))
                .Where(advert => advert.AvailableTimes.Any(time => (time.Date.Hour >= filter.StartTime && time.Date.Hour <= filter.EndTime) && time.AvailabilityStateId == 1))
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Where(advert => advert.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
            }
            //16-0000
            return await _constructionMachineryDbContext.Set<Advert>().Include(advert => advert.Images).Include(advert => advert.Reviews)
                .Where(advert => EF.Functions.Like(advert.Name, "%" + name + "%"))
                .Where(advert => EF.Functions.Like(advert.Description, "%" + filter.KeyWord + "%"))
                .OrderBy(advert => advert.EditDate).Where(advert => advert.UserId == id).Skip(page*TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }
    }
}
