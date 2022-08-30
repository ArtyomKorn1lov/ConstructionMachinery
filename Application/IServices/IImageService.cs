using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;

namespace Application.IServices
{
    public interface IImageService
    {
        Task<List<ImageCommand>> GetByAdvertId(int id);
        Task<ImageCommand> GetById(int id);
        Task<bool> Create(ImageCommandCreate image);
        Task<bool> Remove(int id, string path);
    }
}
