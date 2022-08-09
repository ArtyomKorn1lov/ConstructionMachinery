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
        Task<AvailabilityRequest> GetByUserId(int id);
        Task<AvailabilityRequest> GetByAdvertId(int id);
        Task Create(AvailabilityRequest availabilityRequest);
        Task Remove(int id);
    }
}
