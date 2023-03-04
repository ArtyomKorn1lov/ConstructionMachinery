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
                AverageRating = commands.AverageRating,
                EditDate = commands.EditDate,
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
                DateIssue = command.DateIssue,
                PTS = command.PTS,
                VIN = command.VIN,
                Description = command.Description,
                PublishDate = command.PublishDate,
                EditDate = command.EditDate,
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

        public static AdvertModelDetail AdvertCommandDetailConvertAdvertModelDetail(AdvertCommandDetail command)
        {
            if (command == null)
                return null;
            return new AdvertModelDetail
            {
                Id = command.Id,
                Name = command.Name,
                DateIssue = command.DateIssue,
                PTS = command.PTS,
                VIN = command.VIN,
                Description = command.Description,
                PublishDate = command.PublishDate,
                EditDate = command.EditDate,
                Price = command.Price,
                UserName = command.UserName,
                Images = command.Images.Select(image => new ImageModel
                {
                    Id = image.Id,
                    Path = image.Path,
                    RelativePath = image.RelativePath,
                    AdvertId = image.AdvertId
                }).ToList(),
                AvailableDays = command.AvailableDays.Select(availableDay => new AvailableDayModel
                {
                    Date = availableDay.Date,
                    Times = availableDay.Times.Select(availableTime => new AvailableTimeModel
                    {
                        Id = availableTime.Id,
                        Date = availableTime.Date,
                        AdvertId = availableTime.AdvertId,
                        AvailabilityStateId = availableTime.AvailabilityStateId
                    }).ToList()
                }).ToList()
            };
        }

        public static AdvertCommandCreate AdvertModelCreateConvertAdvertCommandCreate(AdvertModelCreate model)
        {
            if (model == null)
                return null;
            return new AdvertCommandCreate
            {
                Name = model.Name,
                DateIssue = model.DateIssue,
                PTS = model.PTS,
                VIN = model.VIN,
                Description = model.Description,
                Price = model.Price,
                UserId = model.UserId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                StartTime = model.StartTime,
                EndTime = model.EndTime
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
                DateIssue = model.DateIssue,
                PTS = model.PTS,
                VIN = model.VIN,
                Description = model.Description,
                Price = model.Price,
                UserId = model.Id,
                ImageCommands = model.Images.Select(image => new ImageCommand
                {
                    Id = image.Id,
                    AdvertId = image.AdvertId,
                    Path = image.Path,
                    RelativePath = image.RelativePath
                }).ToList(),
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                StartTime = model.StartTime,
                EndTime = model.EndTime
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
                DateIssue = model.DateIssue,
                PTS = model.PTS,
                VIN = model.VIN,
                Description = model.Description,
                Price = model.Price,
                UserId = model.Id,
                Images = model.ImageCommands.Select(image => new ImageModel
                {
                    Id = image.Id,
                    AdvertId = image.AdvertId,
                    Path = image.Path,
                    RelativePath = image.RelativePath
                }).ToList(),
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                StartTime = model.StartTime,
                EndTime = model.EndTime
            };
        }

        public static AdvertModelForRequest AdvertCommandForRequestConvertModel(AdvertCommandForRequest command)
        {
            if (command == null)
                return null;
            return new AdvertModelForRequest
            {
                Id = command.Id,
                Name = command.Name,
                EditDate = command.EditDate,
                Images = command.Images.Select(image => new ImageModel
                {
                    Id = image.Id,
                    Path = image.Path,
                    RelativePath = image.RelativePath,
                    AdvertId = image.AdvertId
                }).ToList(),
            };
        }
    }
}
