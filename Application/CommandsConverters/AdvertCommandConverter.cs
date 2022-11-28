﻿using System;
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
                DateIssue = advert.DateIssue,
                PTS = advert.PTS,
                VIN = advert.VIN,
                Price = advert.Price,
                PublishDate = advert.PublishDate,
                EditDate = advert.EditDate,
                UserId = advert.UserId
            };
        }

        public static AdvertCommandList AdvertEntityConvertToAdvertCommandList(Advert advert, double averageRating)
        {
            if (advert == null)
                return null;
            return new AdvertCommandList
            {
                Id = advert.Id,
                Name = advert.Name,
                Price = advert.Price,
                AverageRating = averageRating,
                EditDate = advert.EditDate,
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
                DateIssue = advert.DateIssue,
                PTS = advert.PTS,
                VIN = advert.VIN,
                Description = advert.Description,
                PublishDate = advert.PublishDate,
                EditDate = advert.EditDate,
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
                DateIssue = advert.DateIssue,
                PTS = advert.PTS,
                VIN = advert.VIN,
                Description = advert.Description,
                EditDate = advert.EditDate,
                Price = advert.Price,
                UserId = advert.UserId,
                ImageCommands = advert.Images.Select(image => new ImageCommand
                {
                    Id = image.Id,
                    AdvertId = image.AdvertId,
                    Path = image.Path,
                    RelativePath = image.RelativePath
                }).ToList()
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
                DateIssue = advert.DateIssue,
                PTS = advert.PTS,
                VIN = advert.VIN,
                Description = advert.Description,
                EditDate = advert.EditDate,
                Price = advert.Price,
                UserId = advert.UserId,
                Images = advert.ImageCommands.Select(image => new Image
                {
                    Id= image.Id,
                    AdvertId = image.AdvertId,
                    Path = image.Path,
                    RelativePath = image.RelativePath
                }).ToList()
            };
        }

        public static AdvertCommandForRequest AdvertConvertToAdvertCommandForRequest(Advert advert)
        {
            if (advert == null)
                return null;
            return new AdvertCommandForRequest
            {
                Id = advert.Id,
                Name = advert.Name,
                EditDate = advert.EditDate,
                Images = advert.Images.Select(image => new ImageCommand
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
