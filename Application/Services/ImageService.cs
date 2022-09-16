using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using Application.IServices;
using Application.CommandsConverters;
using Domain.IRepositories;
using Domain.Entities;
using System.IO;

namespace Application.Services
{
    public class ImageService : IImageService
    {
        private IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<bool> Create(ImageCommandCreate image)
        {
            try
            {
                if(image == null)
                    return false;
                await _imageRepository.Create(ImageCommandConverter.ImageCommandCreateConvertToEntity(image));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ImageCommand>> GetByAdvertId(int id)
        {
            try
            {
                if (id == 0)
                    return null;
                List<Image> images = await _imageRepository.GetByAdvertId(id);
                List<ImageCommand> commands = images.Select(image => ImageCommandConverter.EntityConvertToImageCommand(image)).ToList();
                return commands;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ImageCommand> GetById(int id)
        {
            try
            {
                if (id == 0)
                    return null;
                Image image = await _imageRepository.GetById(id);
                return ImageCommandConverter.EntityConvertToImageCommand(image);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Remove(int id, string path)
        {
            try
            {
                if (id == 0)
                    return false;
                if (path == null)
                    return false;
                await _imageRepository.Remove(id);
                Directory.Delete(path, true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(ImageCommand image)
        {
            try
            {
                if(image != null)
                {
                    await _imageRepository.Update(ImageCommandConverter.ImageCommandConvertToEntity(image));
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
