using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.IRepositories;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private ConstructionMachineryDbContext _constructionMachineryDbContext;
        private const int TAKE_COUNT = 4;

        public ReviewRepository(ConstructionMachineryDbContext constructionMachineryDbContext)
        {
            _constructionMachineryDbContext = constructionMachineryDbContext;
        }

        public async Task Create(Review review)
        {
            await _constructionMachineryDbContext.Set<Review>().AddAsync(review);
        }

        public async Task<List<Review>> GetByAdvertId(int id, int page)
        {
            return await _constructionMachineryDbContext.Set<Review>().Where(review => review.AdvertId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task<Review> GetById(int id)
        {
            return await _constructionMachineryDbContext.Set<Review>().FirstOrDefaultAsync(review => review.Id == id);
        }

        public async Task<List<Review>> GetByUserId(int id, int page)
        {
            return await _constructionMachineryDbContext.Set<Review>().Where(review => review.UserId == id).Skip(page * TAKE_COUNT).Take(TAKE_COUNT).ToListAsync();
        }

        public async Task Remove(int id)
        {
            Review review = await GetById(id);
            if(review != null)
                _constructionMachineryDbContext.Set<Review>().Remove(review);
        }

        public async Task Update(Review review)
        {
            Review _review = await GetById(review.Id);
            _review.CopyFrom(review);
        }
    }
}
