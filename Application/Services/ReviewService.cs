using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using Application.IServices;
using Domain.Entities;
using Domain.IRepositories;
using Application.CommandsConverters;

namespace Application.Services
{
    public class ReviewService : IReviewService
    {
        private IReviewRepository _reviewRepository;
        private IAdvertRepository _advertRepository;

        public ReviewService(IReviewRepository reviewRepository, IAdvertRepository advertRepository)
        {
            _reviewRepository = reviewRepository;
            _advertRepository = advertRepository;
        }

        public async Task<bool> Create(ReviewCommandCreate review)
        {
            try
            {
                if (review == null)
                    return false;
                if (review.ReviewStateId < 1 && review.ReviewStateId > 5)
                    return false;
                if (review.AdvertId <= 0)
                    return false;
                Advert advert = await _advertRepository.GetById(review.AdvertId);
                if (advert == null)
                    return false;
                if (advert.UserId == review.UserId)
                    return false;
                review.Date = review.Date.ToLocalTime();
                await _reviewRepository.Create(ReviewCommandConverter.ReviewCommandCreateConvertToEntity(review));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ReviewCommand>> GetByAdvertId(int id, int page)
        {
            try
            {
                if (id <= 0 || page < 0)
                    return null;
                List<Review> reviews = await _reviewRepository.GetByAdvertId(id, page);
                List<ReviewCommand> commands = reviews.Select(review => ReviewCommandConverter.ReviewEntityConvertToCommand(review)).ToList();
                return commands;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ReviewCommand> GetById(int id)
        {
            try
            {
                if (id <= 0)
                    return null;
                ReviewCommand review = ReviewCommandConverter.ReviewEntityConvertToCommand(await _reviewRepository.GetById(id));
                return review;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ReviewCommand>> GetByUserId(int id, int page)
        {
            try
            {
                if (id <= 0 || page < 0)
                    return null;
                List<Review> reviews = await _reviewRepository.GetByUserId(id, page);
                List<ReviewCommand> commands = reviews.Select(review => ReviewCommandConverter.ReviewEntityConvertToCommand(review)).ToList();
                return commands;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                if (id <= 0)
                    return false;
                await _reviewRepository.Remove(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(ReviewCommandUpdate review)
        {
            try
            {
                if (review == null)
                    return false;
                if (review.UserId <= 0)
                    return false;
                if (review.ReviewStateId <= 1 && review.ReviewStateId >= 5)
                    return false;
                if (review.AdvertId <= 0)
                    return false;
                Advert advert = await _advertRepository.GetById(review.AdvertId);
                if (advert == null)
                    return false;
                if (advert.UserId == review.UserId)
                    return false;
                ReviewCommand currentReview = await GetById(review.Id);
                if (currentReview.UserId != review.UserId)
                    return false;
                review.Date = review.Date.ToLocalTime();
                await _reviewRepository.Update(ReviewCommandConverter.ReviewCommandUpdateConvertToEntity(review));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
