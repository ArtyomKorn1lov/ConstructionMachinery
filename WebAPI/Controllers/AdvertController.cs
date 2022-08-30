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

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/advert")]
    public class AdvertController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IAdvertService _advertService;
        private IAccountService _accountService;

        public AdvertController(IUnitOfWork unitOfWork, IAdvertService advertService, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _advertService = advertService;
            _accountService = accountService;
        }

        [HttpGet("adverts")]
        public async Task<List<AdvertModelList>> GetAll()
        {
            if(HttpContext.User.Identity.Name != null)
            {
                int userId = await _accountService.GetIdByEmail(HttpContext.User.Identity.Name);
                List<AdvertCommandList> advertUserCommands = await _advertService.GetAllWithoutUserId(userId);
                List<AdvertModelList> advertUserModels = advertUserCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
                if (advertUserCommands == null)
                    return null;
                return advertUserModels;
            }
            List<AdvertCommandList> advertCommands = await _advertService.GetAll();
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

        [HttpGet("by-name/{name}")]
        public async Task<List<AdvertModelList>> GetByName(string name)
        {
            if (HttpContext.User.Identity.Name != null)
            {
                int userId = await _accountService.GetIdByEmail(HttpContext.User.Identity.Name);
                List<AdvertCommandList> advertUserCommands = await _advertService.GetByNameWithoutUserId(name, userId);
                List<AdvertModelList> advertUserModels = advertUserCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
                if (advertUserModels == null)
                    return null;
                return advertUserModels;
            }
            List<AdvertCommandList> advertCommands = await _advertService.GetByName(name);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand => 
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
            if (advertModels == null)
                return null;
            return advertModels;
        }

        [Authorize]
        [HttpGet("by-user")]
        public async Task<List<AdvertModelList>> GetByUser()
        {
            int userId = await _accountService.GetIdByEmail(HttpContext.User.Identity.Name);
            List<AdvertCommandList> advertCommands = await _advertService.GetByUserId(userId);
            List<AdvertModelList> advertModels = advertCommands.Select(advertCommand =>
                AdvertModelConverter.AdvertCommandListConvertAdvertModelList(advertCommand)).ToList();
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
                    return Ok("error");
                advertModel.UserId = await _accountService.GetIdByEmail(HttpContext.User.Identity.Name);
                if (await _advertService.Create(AdvertModelConverter.AdvertModelCreateConvertAdvertCommandCreate(advertModel)))
                {
                    await _unitOfWork.Commit();
                    return Ok("success");
                }
                return Ok("error");
            }
            catch
            {
                return BadRequest("error");
            }
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update(AdvertModelUpdate advertModel)
        {
            try
            {
                if (advertModel == null)
                    return Ok("error");
                if (await _advertService.Update(AdvertModelConverter.AdvertModelUpdateConvertAdvertCommandUpdate(advertModel)))
                {
                    await _unitOfWork.Commit();
                    return Ok("success");
                }
                return Ok("error");
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
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                int advertUserId = await _advertService.GetUserIdByAdvert(id);
                int userId = await _accountService.GetIdByEmail(HttpContext.User.Identity.Name);
                if(advertUserId == 0)
                {
                    return Ok("error");
                }
                if(advertUserId != userId)
                {
                    return Ok("error");
                }
                if (await _advertService.Remove(id))
                {
                    await _unitOfWork.Commit();
                    return Ok("success");
                }
                return Ok("error");
            }
            catch
            {
                return BadRequest("error");
            }
        }
    }
}
