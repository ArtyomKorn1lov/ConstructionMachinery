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
        Task<List<AvailabilityRequest>> GetByUserId(int id);
        Task<List<AvailableTime>> GetTimesByAdvertId(int id);
        Task<List<AvailableTime>> GetTimesForRequestByAdvertId(int id);
        Task Create(AvailabilityRequest availabilityRequest);
        Task Confirm(AvailabilityRequest availabilityRequest);
        Task Remove(int id);
        Task<AvailabilityRequest> GetById(int id);
        Task UpdateTime(int id, int state);
    }
}
