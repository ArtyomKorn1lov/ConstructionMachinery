using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using Domain.Entities;

namespace Application.CommandsConverters
{
    public static class AdvertCommandConverter
    {
        public static Advert AdvertCommandCreateConvertToAdvertEntity(AdvertCommandCreate advert)
        {
            if(advert == null)
                return null;
            return new Advert
            {
                Name = advert.Name,
                Description = advert.Description,
                Price = advert.Price,
                UserId = advert.UserId,
                AvailableTimes = advert.AvailableTimeCommandCreates.Select( availableTime => new AvailableTime
                {
                    Date = availableTime.Date,
                    AvailabilityStateId = availableTime.AvailabilityStateId
                } ).ToList(),
            };
        }

        public static AdvertCommandList AdvertEntityConvertToAdvertCommandList(Advert advert)
        {
            if (advert == null)
                return null;
            return new AdvertCommandList
            {
                Id = advert.Id,
                Name = advert.Name,
                Price = advert.Price,
                Images = advert.Images.Select(image => new ImageCommand
                {
                    Id = image.Id,
                    Path = image.Path,
                    RelativePath = image.RelativePath,
                    AdvertId = image.AdvertId
                }).ToList()
            };
        }

        public static AdvertCommandInfo AdvertEntityConvertToAdvertCommandInfo(Advert advert, string name)
        {
            if (advert == null)
                return null;
            return new AdvertCommandInfo
            {
                Id = advert.Id,
                Name = advert.Name,
                Description = advert.Description,
                Price = advert.Price,
                UserName = name,
                Images = advert.Images.Select(image => new ImageCommand
                {
                    Id = image.Id,
                    Path = image.Path,
                    RelativePath = image.RelativePath,
                    AdvertId = image.AdvertId
                }).ToList(),
                AvailableTimes = advert.AvailableTimes.Select(availableTime => new AvailableTimeCommand
                {
                    Id = availableTime.Id,
                    Date = availableTime.Date,
                    AdvertId = availableTime.AdvertId,
                    AvailabilityStateId = availableTime.AvailabilityStateId
                }).ToList(),
            };
        }

        public static AdvertCommandUpdate AdvertEntityConvertToAdvertCommandUpdate(Advert advert)
        {
            if (advert == null)
                return null;
            return new AdvertCommandUpdate
            {
                Id = advert.Id,
                Name = advert.Name,
                Description = advert.Description,
                Price = advert.Price,
                UserId = advert.UserId,
                AvailableTimeCommands = advert.AvailableTimes.Select(availableTime => new AvailableTimeCommand
                {
                    Id = availableTime.Id,
                    Date = availableTime.Date,
                    AdvertId = availableTime.AdvertId,
                    AvailabilityStateId = availableTime.AvailabilityStateId
                }).ToList(),
            };
        }

        public static Advert AdvertCommandUpdateConvertToAdvertEntity(AdvertCommandUpdate advert)
        {
            if (advert == null)
                return null;
            return new Advert
            {
                Id = advert.Id,
                Name = advert.Name,
                Description = advert.Description,
                Price = advert.Price,
                UserId = advert.UserId,
                AvailableTimes = advert.AvailableTimeCommands.Select(availableTime => new AvailableTime
                {
                    Id = availableTime.Id,
                    Date = availableTime.Date,
                    AdvertId = availableTime.AdvertId,
                    AvailabilityStateId = availableTime.AvailabilityStateId
                }).ToList(),
            };
        }
    }
}
