using Application.Commands;
using System.Linq;
using WebAPI.Models;

namespace WebAPI.ModelsConverters
{
    public static class AdvertModelConverter
    {
        public static AdvertModelList AdvertCommandListConvertAdvertModelList(AdvertCommandList commands)
        {
            if (commands == null)
                return null;
            return new AdvertModelList
            {
                Id = commands.Id,
                Name = commands.Name,
                Price = commands.Price,
                Images = commands.Images.Select(image => new ImageModel
                {
                    Id = image.Id,
                    Path = image.Path,
                    RelativePath = image.RelativePath,
                    AdvertId = image.AdvertId
                }).ToList()
            };
        }

        public static AdvertModelInfo AdvertCommandInfoConvertAdvertModelInfo(AdvertCommandInfo command)
        {
            if (command == null)
                return null;
            return new AdvertModelInfo
            {
                Id = command.Id,
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                UserName = command.UserName,
                Images = command.Images.Select(image => new ImageModel
                {
                    Id = image.Id,
                    Path = image.Path,
                    RelativePath = image.RelativePath,
                    AdvertId = image.AdvertId
                }).ToList(),
                AvailableTimes = command.AvailableTimes.Select(command => new AvailableTimeModel
                {
                    Id = command.Id,
                    Date = command.Date,
                    AvailabilityStateId = command.AvailabilityStateId,
                    AdvertId = command.AdvertId
                }).ToList(),
            };
        }

        public static AdvertCommandCreate AdvertModelCreateConvertAdvertCommandCreate(AdvertModelCreate model)
        {
            if (model == null)
                return null;
            return new AdvertCommandCreate
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                UserId = model.UserId,
                AvailableTimeCommandCreates = model.AvailableTimeModelsCreates.Select(command => new AvailableTimeCommandCreate
                {
                    Date = command.Date,
                    AvailabilityStateId = command.AvailabilityStateId
                }).ToList(),
            };
        }

        public static AdvertCommandUpdate AdvertModelUpdateConvertAdvertCommandUpdate(AdvertModelUpdate model)
        {
            if (model == null)
                return null;
            return new AdvertCommandUpdate
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                UserId = model.Id,
                AvailableTimeCommands = model.AvailableTimeModels.Select(command => new AvailableTimeCommand
                {
                    Id = command.Id,
                    Date = command.Date,
                    AvailabilityStateId = command.AvailabilityStateId,
                    AdvertId = command.AdvertId
                }).ToList()
            };
        }

        public static AdvertModelUpdate AdvertCommandUpdateConvertAdvertModelUpdate(AdvertCommandUpdate model)
        {
            if (model == null)
                return null;
            return new AdvertModelUpdate
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                UserId = model.Id,
                AvailableTimeModels = model.AvailableTimeCommands.Select(command => new AvailableTimeModel
                {
                    Id = command.Id,
                    Date = command.Date,
                    AvailabilityStateId = command.AvailabilityStateId,
                    AdvertId = command.AdvertId
                }).ToList()
            };
        }
    }
}
