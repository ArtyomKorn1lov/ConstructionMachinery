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

        public async Task<List<AvailableTime>> GetByAdvertId(int id)
        {
            return await _constructionMachineryDbContext.Set<AvailableTime>()
                .Where(a => a.AdvertId == id && a.AvailabilityRequestId != null).ToListAsync();
        }

        public async Task<AvailabilityRequest> GetById(int id)
        {
            return await _constructionMachineryDbContext.Set<AvailabilityRequest>()
                .Include(availabilityRequest => availabilityRequest.AvailableTimes)
                .FirstOrDefaultAsync(availabilityRequest => availabilityRequest.Id == id);
        }

        public async Task<List<AvailabilityRequest>> GetByUserId(int id)
        {
            return await _constructionMachineryDbContext.Set<AvailabilityRequest>()
                .Include(availabilityRequest => availabilityRequest.AvailableTimes)
                .Where(availabilityRequest => availabilityRequest.UserId == id).ToListAsync();
        }

        public async Task Remove(int id)
        {
            AvailabilityRequest availabilityRequest = await GetById(id);
            if (availabilityRequest != null)
                _constructionMachineryDbContext.Set<AvailabilityRequest>().Remove(availabilityRequest);
        }

        public async Task UpdateTime(int id, int state)
        {
            AvailableTime availableTime = await _constructionMachineryDbContext.Set<AvailableTime>().FirstOrDefaultAsync(availableTime => availableTime.Id == id);
            availableTime.AvailabilityStateId = state;
        }
    }
}
