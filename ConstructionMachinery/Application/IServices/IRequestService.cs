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
        Task<List<AvailabilityRequestCommandForCustomer>> GetForCustomer(int id);
        Task<List<AvailabilityRequestCommandForLandlord>> GetForLandlord(int id);
        Task<bool> Create(AvailabilityRequestCommandCreate request);
        Task<bool> Confirm(int id, int stateId);
        Task<bool> Remove(int id);
    }
}