using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using Domain.Entities;

namespace Application.CommandsConverters
{
    public static class ReviewCommandConverter
    { 
        public static Review ReviewCommandCreateConvertToEntity(ReviewCommandCreate review)
        {
            if (review == null)
                return null;
            return new Review
            {
                Description = review.Description,
                Date = review.Date,
                ReviewStateId = review.ReviewStateId,
                UserId = review.UserId,
                AdvertId = review.AdvertId
            };
        }

        public static Review ReviewCommandUpdateConvertToEntity(ReviewCommandUpdate review)
        {
            if (review == null)
                return null;
            return new Review
            {
                Id = review.Id,
                Description = review.Description,
                Date = review.Date,
                ReviewStateId = review.ReviewStateId,
                UserId = review.UserId,
                AdvertId = review.AdvertId
            };
        }

        public static ReviewCommand ReviewEntityConvertToCommand(Review review)
        {
            if (review == null)
                return null;
            return new ReviewCommand
            {
                Id = review.Id,
                Description = review.Description,
                Date = review.Date,
                ReviewStateId = review.ReviewStateId
            };
        }
    }
}
