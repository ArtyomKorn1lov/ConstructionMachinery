using Application;
using Microsoft.AspNetCore.Mvc;
using Application.IServices;
using WebAPI.Models;
using Application.Commands;
using WebAPI.ModelsConverters;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/advert")]
    public class AdvertController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IAdvertService _advertService;
        private IAccountService _accountService;
        private IImageService _imageService;
        private IWebHostEnvironment _appEnvironment;

        public AdvertController(IUnitOfWork unitOfWork, IAdvertService advertService, IAccountService accountService, IWebHostEnvironment appEnvironment, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _advertService = advertService;
            _accountService = accountService;
            _appEnvironment = appEnvironment;
            _imageService = imageService;
        }

        [HttpGet("adverts/{count}")]
        public async Task<List<AdvertModelList>> GetAll(int count)
        {
            if(User.Identity.Name != null)
            {
                int userId = await _accountService.GetIdByEmail(User.Identity.Name);
                List<AdvertCommandList> advertUserCommands = await _advertService.GetAllWithoutUserId(userId, count);
                List<AdvertModelList> advertUserModels = advertUserCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
                if (advertUserCommands == null)
                    return null;
                return advertUserModels;
            }
            List<AdvertCommandList> advertCommands = await _advertService.GetAll(count);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand => 
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertCommands == null)
                return null;
            return advertModels;
        }

        [HttpGet("by-id/{id}")]
        public async Task<AdvertModelInfo> GetById(int id)
        {
            AdvertModelInfo advert = AdvertModelConverter.AdvertCommandInfoConvertAdvertModelInfo(await _advertService.GetById(id));
            if (advert == null)
                return null;
            return advert;
        }

        [HttpGet("by-name/{name}/{count}")]
        public async Task<List<AdvertModelList>> GetByName(string name, int count)
        {
            if (User.Identity.Name != null)
            {
                int userId = await _accountService.GetIdByEmail(User.Identity.Name);
                List<AdvertCommandList> advertUserCommands = await _advertService.GetByNameWithoutUserId(name, userId, count);
                List<AdvertModelList> advertUserModels = advertUserCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
                if (advertUserModels == null)
                    return null;
                return advertUserModels;
            }
            List<AdvertCommandList> advertCommands = await _advertService.GetByName(name, count);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand => 
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertModels == null)
                return null;
            return advertModels;
        }

        [Authorize]
        [HttpGet("by-user/{count}")]
        public async Task<List<AdvertModelList>> GetByUser(int count)
        {
            int userId = await _accountService.GetIdByEmail(User.Identity.Name);
            List<AdvertCommandList> advertCommands = await _advertService.GetByUserId(userId, count);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertModels == null)
                return null;
            return advertModels;
        }

        [Authorize]
        [HttpGet("adverts-for-request-customer/{count}")]
        public async Task<List<AdvertModelForRequest>> GetForRequestCustomer(int count)
        {
            List<AdvertCommandForRequest> advertCommands = await _advertService.GetForRequestCustomer(await _accountService.GetIdByEmail(HttpContext.User.Identity.Name), count);
            List<AdvertModelForRequest> advertModels = advertCommands.Select(advertCommand => 
                AdvertModelConverter.AdvertCommandForRequestConvertModel(advertCommand)).ToList();
            if (advertModels == null)
                return null;
            return advertModels;
        }

        [Authorize]
        [HttpGet("adverts-for-request-landlord/{count}")]
        public async Task<List<AdvertModelForRequest>> GetForRequestLandlord(int count)
        {
            List<AdvertCommandForRequest> advertCommands = await _advertService.GetForRequestLandlord(await _accountService.GetIdByEmail(HttpContext.User.Identity.Name), count);
            List<AdvertModelForRequest> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandForRequestConvertModel(advertCommand)).ToList();
            if (advertModels == null)
                return null;
            return advertModels;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create(AdvertModelCreate advertModel)
        {
            try
            {
                if(advertModel == null)
                    return BadRequest("error");
                advertModel.UserId = await _accountService.GetIdByEmail(User.Identity.Name);
                if (await _advertService.Create(AdvertModelConverter.AdvertModelCreateConvertAdvertCommandCreate(advertModel)))
                {
                    await _unitOfWork.Commit();
                    return Ok("success");
                }
                return BadRequest("error");
            }
            catch
            {
                return BadRequest("error");
            }
        }

        [Authorize]
        [HttpGet("for-update/{id}")]
        public async Task<AdvertModelUpdate> GetForUpdate(int id)
        {
            AdvertModelUpdate advert = AdvertModelConverter.AdvertCommandUpdateConvertAdvertModelUpdate(await _advertService.GetForUpdate(id));
            if (advert == null)
                return null;
            return advert;
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update(AdvertModelUpdate advertModel)
        {
            try
            {
                if (advertModel == null)
                    return BadRequest("error");
                int advertUserId = await _advertService.GetUserIdByAdvert(advertModel.Id);
                int userId = await _accountService.GetIdByEmail(User.Identity.Name);
                if (advertUserId != userId)
                    return BadRequest("error");
                advertModel.UserId = userId;
                if (await _advertService.Update(AdvertModelConverter.AdvertModelUpdateConvertAdvertCommandUpdate(advertModel)))
                {
                    await _unitOfWork.Commit();
                    return Ok("success");
                }
                return BadRequest("error");
            }
            catch
            {
                return BadRequest("error");
            }
        }

        [Authorize]
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                int advertUserId = await _advertService.GetUserIdByAdvert(id);
                int userId = await _accountService.GetIdByEmail(User.Identity.Name);
                if(advertUserId == 0)
                {
                    return BadRequest("error");
                }
                if(advertUserId != userId)
                {
                    return BadRequest("error");
                }
                List<ImageCommand> images = await _imageService.GetByAdvertId(id);
                string path = null;
                if (images.Count != 0)
                    path = _appEnvironment.WebRootPath + images[0].RelativePath;
                if (await _advertService.Remove(id))
                {
                    await _unitOfWork.Commit();
                    if (path != null)
                        Directory.Delete(path, true);
                    return Ok("success");
                }
                return BadRequest("error");
            }
            catch
            {
                return BadRequest("error");
            }
        }
    }
}
