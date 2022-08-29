using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using Domain.Entities;

namespace Application.CommandsConverters
{
    public static class RequestCommandConverter
    {
        public static AvailabilityRequest AvailabilityRequestCommandCreateConvertToAvailabilityRequestEntity(AvailabilityRequestCommandCreate request)
        {
            if (request == null)
                return null;
            return new AvailabilityRequest
            {
                Address = request.Address,
                RequestStateId = request.RequestStateId,
                UserId = request.UserId
            };
        }

        public static AvailabilityRequestCommandForCustomer AvailabilityRequestEntityConvertToAvailabilityRequestCommandForCustomer(AvailabilityRequest request,
            string advertName, string phone, string landlordName)
        {
            if (request == null)
                return null;
            return new AvailabilityRequestCommandForCustomer
            {
                Id = request.Id,
                AvertName = advertName,
                Address = request.Address,
                Phone = phone,
                LandlordName = landlordName,
                RequestStateId = request.RequestStateId,
                UserId = request.UserId,
                AvailableTimeCommands = request.AvailableTimes.Select(availableTime => new AvailableTimeCommand
                {
                    Id = availableTime.Id,
                    Date = availableTime.Date,
                    AdvertId = availableTime.AdvertId,
                    AvailabilityStateId = availableTime.AvailabilityStateId,
                }).ToList()
            };
        }
        
        public static AvailabilityRequestCommandForLandlord AvailabilityRequestEntityConvertToAvailabilityRequestCommandForLandlord(AvailabilityRequest request,
            string advertName, string phone, string customerName)
        {
            if (request == null)
                return null;
            return new AvailabilityRequestCommandForLandlord
            {
                Id = request.Id,
                Address = request.Address,
                AdvertName = advertName,
                Phone = phone,
                CustomerName = customerName,
                UserId = request.UserId,
                AvailableTimeCommands = request.AvailableTimes.Select(availableTime => new AvailableTimeCommand
                {
                    Id = availableTime.Id,
                    Date = availableTime.Date,
                    AdvertId = availableTime.AdvertId,
                    AvailabilityStateId = availableTime.AvailabilityStateId,
                }).ToList()
            };
        }

        public static AvailabilityRequestListCommand EntityConvertToAvailabilityRequestListCommand(AvailabilityRequest request, string advertName)
        {
            if (request == null)
                return null;
            return new AvailabilityRequestListCommand
            {
                Id = request.Id,
                name = advertName
            };
        }
    }
}
