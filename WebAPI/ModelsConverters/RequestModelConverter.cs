﻿using WebAPI.Models;
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
                AdvertName = command.AdvertName,
                Address = command.Address,
                Phone = command.Phone,
                LandlordName = command.LandlordName,
                RequestStateId = command.RequestStateId,
                UserId = command.UserId,
                Images = command.Images.Select(image => new ImageModel
                {
                    Id = image.Id,
                    Path = image.Path,
                    RelativePath = image.RelativePath,
                    AdvertId = image.AdvertId
                }).ToList(),
                StartRent = command.StartRent,
                EndRent = command.EndRent,
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
                Images = command.Images.Select(image => new ImageModel
                {
                    Id = image.Id,
                    Path = image.Path,
                    RelativePath = image.RelativePath,
                    AdvertId = image.AdvertId
                }).ToList(),
                StartRent = command.StartRent,
                EndRent = command.EndRent,
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
                UserId = model.UserId
            };
        }

        public static AvailableTimeCommandForCreateRequest AvailableTimeModelForCreateRequestConvertToCommand(AvailableTimeModelForCreateRequest model)
        {
            if (model == null)
                return null;
            return new AvailableTimeCommandForCreateRequest
            {
                Id = model.Id,
                AvailabilityStateId = model.AvailabilityStateId
            };
        }

        public static AvailabilityRequestListModel AvailabilityRequestListCommandConvertAvailabilityRequestListModel(AvailabilityRequestListCommand command)
        {
            if (command == null)
                return null;
            return new AvailabilityRequestListModel
            {
                Id = command.Id,
                Name = command.Name,
                Date = command.Date,
                Images = command.Images.Select(image => new ImageModel
                {
                    Id = image.Id,
                    Path = image.Path,
                    RelativePath = image.RelativePath,
                    AdvertId = image.AdvertId
                }).ToList()
            };
        }

        public static AvailableDayModel CommandConvertToAvailableDayModel(AvailiableDayCommand command)
        {
            if (command == null)
                return null;
            return new AvailableDayModel
            {
                Date = command.Date,
                Times = command.Times.Select(availableTime => new AvailableTimeModel
                {
                    Id = availableTime.Id,
                    Date = availableTime.Date,
                    AdvertId = availableTime.AdvertId,
                    AvailabilityStateId = availableTime.AvailabilityStateId
                }).ToList()
            };
        }
    }
}
