using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IRequestRepository
    {
        Task<List<AvailabilityRequest>> GetByAdvertIdUserIdForCustomer(int id, int userId, int count);
        Task<List<AvailabilityRequest>> GetByAdvertIdUserIdForLandlord(int id, int userId, int count);
        Task<List<AvailableTime>> GetTimesForRequestByAdvertId(int id);
        Task<List<AvailableTime>> GetTimesForRemoveRequestByAdvertId(int id);
        Task<int> GetLastRequestId();
        Task Create(AvailabilityRequest availabilityRequest);
        Task Confirm(AvailabilityRequest availabilityRequest);
        Task Remove(int id);
        Task<AvailabilityRequest> GetById(int id);
        Task UpdateTime(int id, int requestId, int state);
        Task<bool> IsAttention(int userId);
    }
}
