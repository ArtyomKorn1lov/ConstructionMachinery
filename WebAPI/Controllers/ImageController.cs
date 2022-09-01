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
        private string _currentDirectory = "/Files/";
        private string _serverDirectory = "https://localhost:5001";

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
                IFormFile uploadImage = Request.Form.Files[0];
                if (uploadImage == null)
                {
                    return Ok("error");
                }
                int advertId = await _advertService.GetLastAdvertId();
                if (advertId == 0)
                    return Ok("error");
                string folderPath = _appEnvironment.WebRootPath + _currentDirectory + advertId.ToString();
                Directory.CreateDirectory(folderPath);
                string path = folderPath + "/" + advertId.ToString() + System.IO.Path.GetExtension(uploadImage.FileName);
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    await uploadImage.CopyToAsync(fileStream);
                }
                path = _serverDirectory + _currentDirectory + advertId.ToString() + "/" + advertId.ToString() + System.IO.Path.GetExtension(uploadImage.FileName);
                string relativePath = _currentDirectory + advertId.ToString();
                ImageModelCreate image = new ImageModelCreate
                {
                    Path = path,
                    RelativePath = relativePath,
                    AdvertId = advertId
                };
                await _imageService.Create(ImageModelConverter.ModelConvertToImageCommandCreate(image));
                await _unitOfWork.Commit();
                return Ok("success");
            }
            catch
            {
                return BadRequest("error");
            }
        }

        [Authorize]
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveImage(int id)
        {
            try
            {
                if (id == 0)
                    return Ok("error");
                ImageCommand image = await _imageService.GetById(id);
                if (image == null)
                    return Ok("error");
                string path = _appEnvironment.WebRootPath + image.RelativePath;
                await _imageService.Remove(id, path);
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
