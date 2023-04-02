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
    public class RequestRepository : IRequestRepository
    {
        private ConstructionMachineryDbContext _constructionMachineryDbContext;

        public RequestRepository(ConstructionMachineryDbContext constructionMachineryDbContext)
        {
            _constructionMachineryDbContext = constructionMachineryDbContext;
        }

        public async Task Confirm(AvailabilityRequest availabilityRequest)
        {
            AvailabilityRequest _availabilityRequest = await GetById(availabilityRequest.Id);
            _availabilityRequest.CopyFrom(availabilityRequest);
        }

        public async Task Create(AvailabilityRequest availabilityRequest)
        {
            await _constructionMachineryDbContext.Set<AvailabilityRequest>().AddAsync(availabilityRequest);
        }

        public async Task<List<AvailableTime>> GetTimesForRequestByAdvertId(int id)
        {
            return await _constructionMachineryDbContext.Set<AvailableTime>()
                .Where(a => a.AdvertId == id && a.AvailabilityStateId == 1 && a.Date >= DateTime.Now).ToListAsync();
        }

        public async Task<AvailableTime> GetTimeById(int id)
        {
            return await _constructionMachineryDbContext.Set<AvailableTime>().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<AvailabilityRequest> GetById(int id)
        {
            return await _constructionMachineryDbContext.Set<AvailabilityRequest>()
                .Include(availabilityRequest => availabilityRequest.AvailableTimes)
                .FirstOrDefaultAsync(availabilityRequest => availabilityRequest.Id == id);
        }

        public async Task<List<AvailabilityRequest>> GetByAdvertIdUserIdForCustomer(int id, int userId, int count)
        {
            return await _constructionMachineryDbContext.Set<AvailabilityRequest>()
                .Include(availabilityRequest => availabilityRequest.AvailableTimes)
                .Where(availabilityRequest => availabilityRequest.AvailableTimes.Any(time => time.AdvertId == id))
                .Where(availabilityRequest => availabilityRequest.UserId == userId)
                .OrderBy(availabilityRequest => availabilityRequest.Id)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<AvailabilityRequest>> GetByUserIdForLandlord(int userId, int count)
        {
            return await _constructionMachineryDbContext.Set<AvailabilityRequest>()
                .Include(availabilityRequest => availabilityRequest.AvailableTimes)
                .Where(availabilityRequest => availabilityRequest.AvailableTimes.Any(time =>
                _constructionMachineryDbContext.Set<Advert>().FirstOrDefault(advert => advert.AvailableTimes.Any(times => times.Id == time.Id)).UserId == userId))
                .Where(availabilityRequest => availabilityRequest.RequestStateId == 3)
                .OrderBy(availabilityRequest => availabilityRequest.Id)
                .Take(count)
                .ToListAsync();
        }

        public async Task<int> GetLastRequestId()
        {
            return await _constructionMachineryDbContext.Set<AvailabilityRequest>().MaxAsync(request => request.Id);
        }

        public async Task Remove(int id)
        {
            AvailabilityRequest availabilityRequest = await _constructionMachineryDbContext.Set<AvailabilityRequest>()
                .FirstOrDefaultAsync(availabilityRequest => availabilityRequest.Id == id);
            if (availabilityRequest != null)
                _constructionMachineryDbContext.Set<AvailabilityRequest>().Remove(availabilityRequest);
        }

        public async Task UpdateTime(int id, int requestId, int state)
        {
            AvailableTime availableTime = await _constructionMachineryDbContext.Set<AvailableTime>().FirstOrDefaultAsync(availableTime => availableTime.Id == id);
            availableTime.AvailabilityRequestId = requestId;
            availableTime.AvailabilityStateId = state;
        }

        public async Task<List<AvailableTime>> GetTimesForRemoveRequestByAdvertId(int id)
        {
            return await _constructionMachineryDbContext.Set<AvailableTime>()
                .Where(a => a.AdvertId == id && a.AvailabilityRequestId != null).ToListAsync();
        }

        public async Task<bool> IsAttention(int userId)
        {
            return await _constructionMachineryDbContext.Set<Advert>().AnyAsync(advert => advert.AvailableTimes.Any(time =>
            !_constructionMachineryDbContext.Set<AvailabilityRequest>().FirstOrDefault(request => request.Id == time.AvailabilityRequestId).IsAvailable)
            && advert.UserId == userId);
        }
    }
}
