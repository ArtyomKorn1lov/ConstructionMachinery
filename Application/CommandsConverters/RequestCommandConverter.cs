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
        public static AvailabilityRequest AvailabilityRequestCommandCreateConvertToAvailabilityRequestEntity(AvailabilityRequestCommandCreate request, int sum)
        {
            if (request == null)
                return null;
            return new AvailabilityRequest
            {
                Address = request.Address,
                Sum = sum,
                Conditions = request.Conditions,
                RequestStateId = request.RequestStateId,
                UserId = request.UserId
            };
        }

        public static AvailabilityRequestCommandForCustomer AvailabilityRequestEntityConvertToAvailabilityRequestCommandForCustomer(AvailabilityRequest request,
            List<Image> images, string advertName, string phone, string landlordName, DateTime startRent, DateTime endRent)
        {
            if (request == null)
                return null;
            return new AvailabilityRequestCommandForCustomer
            {
                Id = request.Id,
                AdvertName = advertName,
                Created = request.Created,
                Updated = request.Updated,
                Address = request.Address,
                Sum = request.Sum,
                Conditions = request.Conditions,
                Phone = phone,
                LandlordName = landlordName,
                RequestStateId = request.RequestStateId,
                UserId = request.UserId,
                Images = images.Select(image => new ImageCommand
                {
                    Id = image.Id,
                    Path = image.Path,
                    RelativePath = image.RelativePath,
                    AdvertId = image.AdvertId
                }).ToList(),
                StartRent = startRent,
                EndRent = endRent,
            };
        }
        
        public static AvailabilityRequestCommandForLandlord AvailabilityRequestEntityConvertToAvailabilityRequestCommandForLandlord(AvailabilityRequest request,
            List<Image> images, string advertName, string phone, string customerName, DateTime startRent, DateTime endRent)
        {
            if (request == null)
                return null;
            return new AvailabilityRequestCommandForLandlord
            {
                Id = request.Id,
                Created = request.Created,
                Updated = request.Updated,
                Address = request.Address,
                Sum = request.Sum,
                Conditions = request.Conditions,
                AdvertName = advertName,
                Phone = phone,
                CustomerName = customerName,
                UserId = request.UserId,
                Images = images.Select(image => new ImageCommand
                {
                    Id = image.Id,
                    Path = image.Path,
                    RelativePath = image.RelativePath,
                    AdvertId = image.AdvertId
                }).ToList(),
                StartRent = startRent,
                EndRent = endRent,
            };
        }

        public static AvailabilityRequestListCommand EntityConvertToAvailabilityRequestListCommand(AvailabilityRequest request, DateTime date, string advertName, List<Image> images)
        {
            if (request == null)
                return null;
            return new AvailabilityRequestListCommand
            {
                Id = request.Id,
                Name = advertName,
                Date = date,
                Images = images.Select(image => new ImageCommand
                {
                    Id = image.Id,
                    Path = image.Path,
                    RelativePath = image.RelativePath,
                    AdvertId = image.AdvertId
                }).ToList()
            };
        }

        public static AvailableTimeCommand EntityConvertToAvailableTimeCommand(AvailableTime time)
        {
            if (time == null)
                return null;
            return new AvailableTimeCommand
            {
                Id = time.Id,
                Date = time.Date,
                AdvertId = time.AdvertId,
                AvailabilityStateId = time.AvailabilityStateId
            };
        }
    }
}
