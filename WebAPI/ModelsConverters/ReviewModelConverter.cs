using Application.Commands;
using WebAPI.Models;

namespace WebAPI.ModelsConverters
{
    public static class ReviewModelConverter
    {
        public static ReviewCommandCreate ReviewModelCreateConvertToCommand(ReviewModelCreate review)
        {
            if (review == null)
                return null;
            return new ReviewCommandCreate
            {
                Description = review.Description,
                Date = review.Date,
                UserId = review.UserId,
                AdvertId = review.AdvertId,
                ReviewStateId = review.ReviewStateId
            };
        }

        public static ReviewCommandUpdate ReviewModelUpdateConvertToCommand(ReviewModelUpdate review)
        {
            if (review == null)
                return null;
            return new ReviewCommandUpdate
            {
                Id = review.Id,
                Description = review.Description,
                Date = review.Date,
                AdvertId = review.AdvertId,
                UserId = review.UserId,
                ReviewStateId = review.ReviewStateId
            };
        }

        public static ReviewModel ReviewCommandCovertToModel(ReviewCommand review)
        {
            if (review == null)
                return null;
            return new ReviewModel
            {
                Id = review.Id,
                Description = review.Description,
                UserId = review.UserId,
                ReviewStateId = review.ReviewStateId,
                Date = review.Date
            };
        }

        public static ReviewModelInfo ReviewCommandCovertToModelInfo(ReviewCommand review)
        {
            if (review == null)
                return null;
            return new ReviewModelInfo
            {
                Id = review.Id,
                Description = review.Description,
                ReviewStateId = review.ReviewStateId,
                AdvertId = review.AdvertId
            };
        }
    }
}
