﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;

namespace Application.IServices
{
    public interface IRequestService
    {
        Task<AvailabilityRequestCommandForCustomer> GetForCustomer(int id, int userId);
        Task<AvailabilityRequestCommandForLandlord> GetForLandlord(int id, int userId);
        Task<AvailabilityRequestCommandForLandlord> GetForLandlordConfirm(int id, int userId);
        Task<List<AvailabilityRequestListCommand>> GetListForCustomer(int id, int userId, int page);
        Task<List<AvailabilityRequestListCommand>> GetListForLandlord(int userId, int page);
        Task<List<AvailabilityRequestListCommand>> GetListForLandlordConfirm(int id, int userId, int page);
        Task<int> GetLastRequestId();
        Task<LeaseRequestCommand> GetAvailableTimesByAdvertId(int id, int userId);
        Task<bool> Create(AvailabilityRequestCommandCreate request, int timeId, int count);
        Task<bool> UpdateTimes(int requestId, List<AvailableTimeCommandForCreateRequest> times);
        Task<bool> Confirm(int id, int stateId, int userId);
        Task<bool> Remove(int id, int userId);
        Task<bool> IsAttention(int userId);
    }
}
