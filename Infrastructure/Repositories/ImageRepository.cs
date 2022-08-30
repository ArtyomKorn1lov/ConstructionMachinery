using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private ConstructionMachineryDbContext _constructionMachineryDbContext;
        public async Task Create(Image image)
        {
            await _constructionMachineryDbContext.Set<Image>().AddAsync(image);
        }

        public async Task<List<Image>> GetByAdvertId(int id)
        {
            return await _constructionMachineryDbContext.Set<Image>().Where(image => image.AdvertId == id).ToListAsync();
        }

        public async Task<Image> GetById(int id)
        {
            return await _constructionMachineryDbContext.Set<Image>().FirstOrDefaultAsync(image => image.Id == id);
        }

        public async Task Remove(int id)
        {
            Image image = await GetById(id);
            if (image != null)
                _constructionMachineryDbContext.Set<Image>().Remove(image);
        }
    }
}
