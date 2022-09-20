using Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Commands;
using Application.IServices;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using WebAPI.Models;
using WebAPI.ModelsConverters;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/image")]
    public class ImageController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IImageService _imageService;
        private IAdvertService _advertService;
        private IWebHostEnvironment _appEnvironment;
        private const string _currentDirectory = "/Files/";
        private const string _serverDirectory = "https://localhost:5001";

        public ImageController(IUnitOfWork unitOfWork, IImageService imageService, IWebHostEnvironment appEnvironment, IAdvertService advertService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _appEnvironment = appEnvironment;
            _advertService = advertService;
        }

        [Authorize]
        [HttpPost("create")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> AddImage()
        {
            try
            {
                int advertId = await _advertService.GetLastAdvertId();
                if (advertId == 0)
                    return Ok("error");
                string folderPath = _appEnvironment.WebRootPath + _currentDirectory + advertId.ToString();
                Directory.CreateDirectory(folderPath);
                IFormFileCollection files = Request.Form.Files;
                if (files.Any(f => f.Length == 0))
                {
                    return Ok("error");
                }
                int countName = 0;
                foreach (IFormFile uploadImage in files)
                {
                    countName++;
                    string path = folderPath + "/" + (advertId + countName).ToString() + System.IO.Path.GetExtension(uploadImage.FileName);
                    using (FileStream fileStream = new FileStream(path, FileMode.Create))
                    {
                        await uploadImage.CopyToAsync(fileStream);
                    }
                    path = _serverDirectory + _currentDirectory + advertId.ToString() + "/" + (advertId + countName).ToString() + System.IO.Path.GetExtension(uploadImage.FileName);
                    string relativePath = _currentDirectory + advertId.ToString();
                    ImageModelCreate image = new ImageModelCreate
                    {
                        Path = path,
                        RelativePath = relativePath,
                        AdvertId = advertId
                    };
                    await _imageService.Create(ImageModelConverter.ModelConvertToImageCommandCreate(image));
                }
                await _unitOfWork.Commit();
                return Ok("success");
            }
            catch
            {
                return BadRequest("error");
            }
        }

        [Authorize]
        [HttpPut("update/{id}")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UpdateImage(int id)
        {
            try
            {
                string folderPath = _appEnvironment.WebRootPath + _currentDirectory + id.ToString();
                IFormFileCollection files = Request.Form.Files;
                if (files.Any(f => f.Length == 0))
                {
                    return Ok("error");
                }
                AdvertModelInfo advert = AdvertModelConverter.AdvertCommandInfoConvertAdvertModelInfo(await _advertService.GetById(id));
                int countName = 0;
                if(advert.Images.Count != 0)
                {
                    Regex regex = new Regex(@"\/\d+\.", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Match match = regex.Match(advert.Images[advert.Images.Count - 1].Path);
                    string regexResult = match.Value;
                    countName = Int32.Parse(regexResult.Substring(1, regexResult.Length - 2));
                    countName = countName - id;
                }
                foreach (IFormFile uploadImage in files)
                {
                    countName++;
                    string path = folderPath + "/" + (id + countName).ToString() + System.IO.Path.GetExtension(uploadImage.FileName);
                    using (FileStream fileStream = new FileStream(path, FileMode.Create))
                    {
                        await uploadImage.CopyToAsync(fileStream);
                    }
                    path = _serverDirectory + _currentDirectory + id.ToString() + "/" + (id + countName).ToString() + System.IO.Path.GetExtension(uploadImage.FileName);
                    string relativePath = _currentDirectory + id.ToString();
                    ImageModelCreate image = new ImageModelCreate
                    {
                        Path = path,
                        RelativePath = relativePath,
                        AdvertId = id
                    };
                    await _imageService.Create(ImageModelConverter.ModelConvertToImageCommandCreate(image));
                }
                await _unitOfWork.Commit();
                return Ok("success");
            }
            catch
            {
                return BadRequest("error");
            }
        }

        [Authorize]
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveImage(List<int> imagesId)
        {
            try
            {
                if (imagesId == null)
                    return Ok("error");
                foreach(int id in imagesId)
                {
                    ImageCommand image = await _imageService.GetById(id);
                    string path = image.Path;
                    path = path.Replace(_serverDirectory, "");
                    path = _appEnvironment.WebRootPath + path; 
                    await _imageService.Remove(id, path);
                }
                await _unitOfWork.Commit();
                return Ok("success");
            }
            catch
            {
                return BadRequest("error");
            }
        }
    }
}
