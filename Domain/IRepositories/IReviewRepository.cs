using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetByUserId(int id, int count);
        Task<List<Review>> GetByAdvertId(int id, int count);
        Task<Review> GetById(int id);
        Task Create(Review review);
        Task Update(Review review);
        Task Remove(int id);
    }
}
