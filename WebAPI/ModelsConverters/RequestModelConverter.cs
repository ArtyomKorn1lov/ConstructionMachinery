using WebAPI.Models;
using Application.Commands;
using System.Linq;
using WebApi.Models;

namespace WebAPI.ModelsConverters
{
    public static class RequestModelConverter
    {
        public static AvailabilityRequestModelForCustomer AvailabilityRequestForCustomerCommandConvertModel(AvailabilityRequestCommandForCustomer command)
        {
            if (command == null)
                return null;
            return new AvailabilityRequestModelForCustomer
            {
                Id = command.Id,
                AvertName = command.AvertName,
                Address = command.Address,
                Phone = command.Phone,
                LandlordName = command.LandlordName,
                RequestStateId = command.RequestStateId,
                UserId = command.UserId,
                AvailableTimeModels = command.AvailableTimeCommands.Select(model => new AvailableTimeModel
                {
                    Id = model.Id,
                    Date = model.Date,
                    AdvertId = model.AdvertId,
                    AvailabilityStateId = model.AvailabilityStateId
                }).ToList()
            };
        }

        public static AvailabilityRequestModelForLandlord AvailabilityRequestCommandForLandlordConvertModel(AvailabilityRequestCommandForLandlord command)
        {
            if (command == null)
                return null;
            return new AvailabilityRequestModelForLandlord
            {
                Id = command.Id,
                AdvertName = command.AdvertName,
                Address = command.Address,
                Phone = command.Phone,
                CustomerName = command.CustomerName,
                UserId = command.UserId,
                AvailableTimeModels = command.AvailableTimeCommands.Select(model => new AvailableTimeModel
                {
                    Id = model.Id,
                    Date = model.Date,
                    AdvertId = model.AdvertId,
                    AvailabilityStateId = model.AvailabilityStateId
                }).ToList()
            };
        }

        public static AvailabilityRequestCommandCreate AvailabilityRequestModelCreateConvertCommand(AvailabilityRequestModelCreate model)
        {
            if (model == null)
                return null;
            return new AvailabilityRequestCommandCreate
            {
                Address = model.Address,
                RequestStateId = model.RequestStateId,
                UserId = model.UserId,
                AvailableTimeCommandForCreateRequests = model.AvailableTimeModelForCreateRequests.Select(command => new AvailableTimeCommandForCreateRequest
                {
                    Id = command.Id,
                    AvailabilityStateId = command.AvailabilityStateId
                }).ToList()
            };
        }

        public static AvailabilityRequestListModel AvailabilityRequestListCommandConvertAvailabilityRequestListModel(AvailabilityRequestListCommand command)
        {
            if (command == null)
                return null;
            return new AvailabilityRequestListModel
            {
                Id = command.Id,
                name = command.name
            };
        }

        public static AvailableTimeModel CommandConvertToAvailableTimeModel(AvailableTimeCommand command)
        {
            if (command == null)
                return null;
            return new AvailableTimeModel
            {
                Id = command.Id,
                Date = command.Date,
                AdvertId = command.AdvertId,
                AvailabilityStateId = command.AdvertId
            };
        }
    }
}
