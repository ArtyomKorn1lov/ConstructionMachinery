using Application.Commands;
using WebAPI.Models;

namespace WebAPI.ModelsConverters
{
    public static class ImageModelConverter
    {
        public static ImageCommandCreate ModelConvertToImageCommandCreate(ImageModelCreate image)
        {
            if (image == null)
                return null;
            return new ImageCommandCreate
            {
                Path = image.Path,
                AdvertId = image.AdvertId
            };
        }

        public static ImageModel CommandConvertToImageModel(ImageCommand image)
        {
            if (image == null)
                return null;
            return new ImageModel
            {
                Id = image.Id,
                Path = image.Path,
                AdvertId = image.AdvertId
            };
        }
    }
}
