using WebAPI.Models;
using Application.Commands;

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

        public static AvailabilityRequestCommandCreate availabilityRequestModelCreateConvertCommand(AvailabilityRequestModelCreate model)
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
    }
}
