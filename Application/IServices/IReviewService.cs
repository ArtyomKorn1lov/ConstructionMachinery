using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;

namespace Application.IServices
{
    public interface IReviewService
    {
        Task<List<ReviewCommand>> GetByUserId(int id, int page);
        Task<List<ReviewCommand>> GetByAdvertId(int id, int page);
        Task<ReviewCommand> GetById(int id);
        Task<bool> Create(ReviewCommandCreate review);
        Task<bool> Update(ReviewCommandUpdate review);
        Task<bool> Remove(int id);
    }
}
