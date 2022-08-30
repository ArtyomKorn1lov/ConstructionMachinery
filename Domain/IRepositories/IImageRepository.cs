using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IImageRepository
    {
        Task<List<Image>> GetByAdvertId(int id);
        Task<Image> GetById(int id);
        Task Create(Image image);
        Task Remove(int id);
    }
}
