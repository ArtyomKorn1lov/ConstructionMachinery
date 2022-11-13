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

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<bool> Create(ReviewCommandCreate review)
        {
            try
            {
                if(review == null)
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

        public async Task<List<ReviewCommand>> GetByAdvertId(int id, int count)
        {
            try
            {
                List<Review> reviews = await _reviewRepository.GetByAdvertId(id, count);
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
                ReviewCommand review = ReviewCommandConverter.ReviewEntityConvertToCommand(await _reviewRepository.GetById(id));
                return review;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ReviewCommand>> GetByUserId(int id, int count)
        {
            try
            {
                List<Review> reviews = await _reviewRepository.GetByUserId(id, count);
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
                if (id == 0)
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
