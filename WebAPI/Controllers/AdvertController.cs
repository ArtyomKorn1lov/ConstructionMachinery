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

        [HttpGet("adverts/{page}")]
        public async Task<List<AdvertModelList>> GetAll(int page)
        {
            if (User.Identity.Name != null)
            {
                int userId = await _accountService.GetIdByEmail(User.Identity.Name);
                List<AdvertCommandList> advertUserCommands = await _advertService.GetAllWithoutUserId(userId, page);
                List<AdvertModelList> advertUserModels = advertUserCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
                if (advertUserCommands == null)
                    return null;
                return advertUserModels;
            }
            List<AdvertCommandList> advertCommands = await _advertService.GetAll(page);
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

        [HttpGet("detail/{id}")]
        public async Task<AdvertModelDetail> GetDetailAdvert(int id)
        {
            AdvertModelDetail advert = AdvertModelConverter.AdvertCommandDetailConvertAdvertModelDetail(await _advertService.GetDetailAdvert(id));
            if (advert == null)
                return null;
            return advert;
        }

        [HttpGet("by-name/{name}/{page}")]
        public async Task<List<AdvertModelList>> GetByName(string name, int page)
        {
            if (User.Identity.Name != null)
            {
                int userId = await _accountService.GetIdByEmail(User.Identity.Name);
                List<AdvertCommandList> advertUserCommands = await _advertService.GetByNameWithoutUserId(name, userId, page);
                List<AdvertModelList> advertUserModels = advertUserCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
                if (advertUserModels == null)
                    return null;
                return advertUserModels;
            }
            List<AdvertCommandList> advertCommands = await _advertService.GetByName(name, page);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertModels == null)
                return null;
            return advertModels;
        }

        [HttpGet("by-price-max/{page}")]
        public async Task<List<AdvertModelList>> GetSortByPriceMax(int page)
        {
            if (User.Identity.Name != null)
            {
                int userId = await _accountService.GetIdByEmail(User.Identity.Name);
                List<AdvertCommandList> advertUserCommands = await _advertService.GetSortByPriceMaxWithoutUserId(page, userId);
                List<AdvertModelList> advertUserModels = advertUserCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
                if (advertUserCommands == null)
                    return null;
                return advertUserModels;
            }
            List<AdvertCommandList> advertCommands = await _advertService.GetSortByPriceMax(page);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertCommands == null)
                return null;
            return advertModels;
        }

        [HttpGet("by-price-min/{page}")]
        public async Task<List<AdvertModelList>> GetSortByPriceMin(int page)
        {
            if (User.Identity.Name != null)
            {
                int userId = await _accountService.GetIdByEmail(User.Identity.Name);
                List<AdvertCommandList> advertUserCommands = await _advertService.GetSortByPriceMinWithoutUserId(page, userId);
                List<AdvertModelList> advertUserModels = advertUserCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
                if (advertUserCommands == null)
                    return null;
                return advertUserModels;
            }
            List<AdvertCommandList> advertCommands = await _advertService.GetSortByPriceMin(page);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertCommands == null)
                return null;
            return advertModels;
        }

        [HttpGet("by-rating-max/{page}")]
        public async Task<List<AdvertModelList>> GetSortByRatingMax(int page)
        {
            if (User.Identity.Name != null)
            {
                int userId = await _accountService.GetIdByEmail(User.Identity.Name);
                List<AdvertCommandList> advertUserCommands = await _advertService.GetSortByRatingMaxWithoutUserId(page, userId);
                List<AdvertModelList> advertUserModels = advertUserCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
                if (advertUserCommands == null)
                    return null;
                return advertUserModels;
            }
            List<AdvertCommandList> advertCommands = await _advertService.GetSortByRatingMax(page);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertCommands == null)
                return null;
            return advertModels;
        }

        [HttpGet("by-rating-min/{page}")]
        public async Task<List<AdvertModelList>> GetSortByRatingMin(int page)
        {
            if (User.Identity.Name != null)
            {
                int userId = await _accountService.GetIdByEmail(User.Identity.Name);
                List<AdvertCommandList> advertUserCommands = await _advertService.GetSortByRatingMinWithoutUserId(page, userId);
                List<AdvertModelList> advertUserModels = advertUserCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
                if (advertUserCommands == null)
                    return null;
                return advertUserModels;
            }
            List<AdvertCommandList> advertCommands = await _advertService.GetSortByRatingMin(page);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertCommands == null)
                return null;
            return advertModels;
        }

        [HttpGet("by-date-min/{page}")]
        public async Task<List<AdvertModelList>> GetSortByDateMin(int page)
        {
            if (User.Identity.Name != null)
            {
                int userId = await _accountService.GetIdByEmail(User.Identity.Name);
                List<AdvertCommandList> advertUserCommands = await _advertService.GetSortByDateMinWithoutUserId(page, userId);
                List<AdvertModelList> advertUserModels = advertUserCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
                if (advertUserCommands == null)
                    return null;
                return advertUserModels;
            }
            List<AdvertCommandList> advertCommands = await _advertService.GetSortByDateMin(page);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertCommands == null)
                return null;
            return advertModels;
        }

        [HttpGet("by-price-max/{name}/{page}")]
        public async Task<List<AdvertModelList>> GetSortByPriceMaxByName(string name, int page)
        {
            if (User.Identity.Name != null)
            {
                int userId = await _accountService.GetIdByEmail(User.Identity.Name);
                List<AdvertCommandList> advertUserCommands = await _advertService.GetSortByPriceMaxWithoutUserIdByName(page, userId, name);
                List<AdvertModelList> advertUserModels = advertUserCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
                if (advertUserModels == null)
                    return null;
                return advertUserModels;
            }
            List<AdvertCommandList> advertCommands = await _advertService.GetSortByPriceMaxByName(page, name);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertModels == null)
                return null;
            return advertModels;
        }

        [HttpGet("by-price-min/{name}/{page}")]
        public async Task<List<AdvertModelList>> GetSortByPriceMinByName(string name, int page)
        {
            if (User.Identity.Name != null)
            {
                int userId = await _accountService.GetIdByEmail(User.Identity.Name);
                List<AdvertCommandList> advertUserCommands = await _advertService.GetSortByPriceMinWithoutUserIdByName(page, userId, name);
                List<AdvertModelList> advertUserModels = advertUserCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
                if (advertUserModels == null)
                    return null;
                return advertUserModels;
            }
            List<AdvertCommandList> advertCommands = await _advertService.GetSortByPriceMinByName(page, name);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertModels == null)
                return null;
            return advertModels;
        }

        [HttpGet("by-rating-max/{name}/{page}")]
        public async Task<List<AdvertModelList>> GetSortByRatingMaxByName(string name, int page)
        {
            if (User.Identity.Name != null)
            {
                int userId = await _accountService.GetIdByEmail(User.Identity.Name);
                List<AdvertCommandList> advertUserCommands = await _advertService.GetSortByRatingMaxWithoutUserIdByName(page, userId, name);
                List<AdvertModelList> advertUserModels = advertUserCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
                if (advertUserModels == null)
                    return null;
                return advertUserModels;
            }
            List<AdvertCommandList> advertCommands = await _advertService.GetSortByRatingMaxByName(page, name);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertModels == null)
                return null;
            return advertModels;
        }

        [HttpGet("by-rating-min/{name}/{page}")]
        public async Task<List<AdvertModelList>> GetSortByRatingMinByName(string name, int page)
        {
            if (User.Identity.Name != null)
            {
                int userId = await _accountService.GetIdByEmail(User.Identity.Name);
                List<AdvertCommandList> advertUserCommands = await _advertService.GetSortByRatingMinWithoutUserIdByName(page, userId, name);
                List<AdvertModelList> advertUserModels = advertUserCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
                if (advertUserModels == null)
                    return null;
                return advertUserModels;
            }
            List<AdvertCommandList> advertCommands = await _advertService.GetSortByRatingMinByName(page, name);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertModels == null)
                return null;
            return advertModels;
        }

        [HttpGet("by-date-min/{name}/{page}")]
        public async Task<List<AdvertModelList>> GetSortByDateMinByName(string name, int page)
        {
            if (User.Identity.Name != null)
            {
                int userId = await _accountService.GetIdByEmail(User.Identity.Name);
                List<AdvertCommandList> advertUserCommands = await _advertService.GetSortByDateMinWithoutUserIdByName(page, userId, name);
                List<AdvertModelList> advertUserModels = advertUserCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
                if (advertUserModels == null)
                    return null;
                return advertUserModels;
            }
            List<AdvertCommandList> advertCommands = await _advertService.GetSortByDateMinByName(page, name);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertModels == null)
                return null;
            return advertModels;
        }

        [Authorize]
        [HttpGet("by-user/{page}")]
        public async Task<List<AdvertModelList>> GetByUser(int page)
        {
            int userId = await _accountService.GetIdByEmail(User.Identity.Name);
            List<AdvertCommandList> advertCommands = await _advertService.GetByUserId(userId, page);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertModels == null)
                return null;
            return advertModels;
        }

        [Authorize]
        [HttpGet("by-user-price-max/{page}")]
        public async Task<List<AdvertModelList>> GetSortByPriceMaxByUserId(int page)
        {
            int userId = await _accountService.GetIdByEmail(User.Identity.Name);
            List<AdvertCommandList> advertCommands = await _advertService.GetSortByPriceMaxByUserId(userId, page);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertModels == null)
                return null;
            return advertModels;
        }

        [Authorize]
        [HttpGet("by-user-price-min/{page}")]
        public async Task<List<AdvertModelList>> GetSortByPriceMinByUserId(int page)
        {
            int userId = await _accountService.GetIdByEmail(User.Identity.Name);
            List<AdvertCommandList> advertCommands = await _advertService.GetSortByPriceMinByUserId(userId, page);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertModels == null)
                return null;
            return advertModels;
        }

        [Authorize]
        [HttpGet("by-user-rating-max/{page}")]
        public async Task<List<AdvertModelList>> GetSortByRatingMaxByUserId(int page)
        {
            int userId = await _accountService.GetIdByEmail(User.Identity.Name);
            List<AdvertCommandList> advertCommands = await _advertService.GetSortByRatingMaxByUserId(userId, page);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertModels == null)
                return null;
            return advertModels;
        }

        [Authorize]
        [HttpGet("by-user-rating-min/{page}")]
        public async Task<List<AdvertModelList>> GetSortByRatingMinByUserId(int page)
        {
            int userId = await _accountService.GetIdByEmail(User.Identity.Name);
            List<AdvertCommandList> advertCommands = await _advertService.GetSortByRatingMinByUserId(userId, page);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertModels == null)
                return null;
            return advertModels;
        }

        [Authorize]
        [HttpGet("by-user-date-min/{page}")]
        public async Task<List<AdvertModelList>> GetSortByDateMinByUserId(int page)
        {
            int userId = await _accountService.GetIdByEmail(User.Identity.Name);
            List<AdvertCommandList> advertCommands = await _advertService.GetSortByDateMinByUserId(userId, page);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertModels == null)
                return null;
            return advertModels;
        }

        [Authorize]
        [HttpGet("adverts-for-request-customer/{page}")]
        public async Task<List<AdvertModelForRequest>> GetForRequestCustomer(int page)
        {
            List<AdvertCommandForRequest> advertCommands = await _advertService.GetForRequestCustomer(await _accountService.GetIdByEmail(HttpContext.User.Identity.Name), page);
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
                if (advertModel == null)
                    return BadRequest("error");
                advertModel.UserId = await _accountService.GetIdByEmail(User.Identity.Name);
                if (!await _advertService.Create(AdvertModelConverter.AdvertModelCreateConvertAdvertCommandCreate(advertModel)))
                    return BadRequest("error");
                await _unitOfWork.Commit();
                return Ok("success");
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
            int userId = await _accountService.GetIdByEmail(User.Identity.Name);
            AdvertModelUpdate advert = AdvertModelConverter.AdvertCommandUpdateConvertAdvertModelUpdate(await _advertService.GetForUpdate(id, userId));
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
                if (!await _advertService.Update(AdvertModelConverter.AdvertModelUpdateConvertAdvertCommandUpdate(advertModel)))
                    return BadRequest("error");
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
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                int advertUserId = await _advertService.GetUserIdByAdvert(id);
                int userId = await _accountService.GetIdByEmail(User.Identity.Name);
                if (advertUserId == 0)
                    return BadRequest("error");
                if (advertUserId != userId)
                    return BadRequest("error");
                List<ImageCommand> images = await _imageService.GetByAdvertId(id);
                string path = null;
                if (images.Count != 0)
                    path = _appEnvironment.WebRootPath + images[0].RelativePath;
                if (!await _advertService.Remove(id))
                    return BadRequest("error");
                await _unitOfWork.Commit();
                if (path != null)
                    Directory.Delete(path, true);
                return Ok("success");
            }
            catch
            {
                return BadRequest("error");
            }
        }
    }
}
