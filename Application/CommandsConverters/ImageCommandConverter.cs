using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Commands;

namespace Application.CommandsConverters
{
    public static class ImageCommandConverter
    {
        public static ImageCommand EntityConvertToImageCommand(Image image)
        {
            if (image == null)
                return null;
            return new ImageCommand
            {
                Id = image.Id,
                Path = image.Path,
                RelativePath = image.RelativePath,
                AdvertId = image.AdvertId
            };
        }

        public static Image ImageCommandConvertToEntity(ImageCommand image)
        {
            if (image == null)
                return null;
            return new Image
            {
                Id = image.Id,
                Path = image.Path,
                RelativePath = image.RelativePath,
                AdvertId = image.AdvertId
            };
        }

        public static Image ImageCommandCreateConvertToEntity(ImageCommandCreate image)
        {
            if (image == null)
                return null;
            return new Image
            {
                Path = image.Path,
                RelativePath = image.RelativePath,
                AdvertId = image.AdvertId
            };
        }
    }
}
