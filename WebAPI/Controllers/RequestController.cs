using Microsoft.AspNetCore.Mvc;
using Application.Commands;
using Application.IServices;
using Application;
using WebAPI.Models;
using WebAPI.ModelsConverters;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/request")]
    public class RequestController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IRequestService _requestService;
        private IAccountService _accountService;

        public RequestController(IUnitOfWork unitOfWork, IRequestService requestService, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _requestService = requestService;
            _accountService = accountService;
        }

        [Authorize]
        [HttpGet("customer/{id}")]
        public async Task<AvailabilityRequestModelForCustomer> GetForCustomer(int id)
        {
            AvailabilityRequestCommandForCustomer command = await _requestService.GetForCustomer(id, await _accountService.GetIdByEmail(HttpContext.User.Identity.Name));
            AvailabilityRequestModelForCustomer model = RequestModelConverter.AvailabilityRequestForCustomerCommandConvertModel(command);
            if (model == null)
                return null;
            return model;
        }

        [Authorize]
        [HttpGet("landlord/{id}")]
        public async Task<AvailabilityRequestModelForLandlord> GetForLandlord(int id)
        {
            AvailabilityRequestCommandForLandlord command = await _requestService.GetForLandlord(id, await _accountService.GetIdByEmail(HttpContext.User.Identity.Name));
            AvailabilityRequestModelForLandlord model = RequestModelConverter.AvailabilityRequestCommandForLandlordConvertModel(command);
            if (model == null)
                return null;
            return model;
        }

        [Authorize]
        [HttpGet("for-customer")]
        public async Task<List<AvailabilityRequestListModel>> GetListForCustomer()
        {
            List<AvailabilityRequestListCommand> commands = await _requestService.GetListForCustomer(await _accountService.GetIdByEmail(HttpContext.User.Identity.Name));
            List<AvailabilityRequestListModel> models = commands.Select(command => 
                RequestModelConverter.AvailabilityRequestListCommandConvertAvailabilityRequestListModel(command)).ToList();
            if (models == null)
                return null;
            return models;
        }

        [Authorize]
        [HttpGet("for-landlord")]
        public async Task<List<AvailabilityRequestListModel>> GetListForLandlord()
        {
            List<AvailabilityRequestListCommand> commands = await _requestService.GetListForLandlord(await _accountService.GetIdByEmail(HttpContext.User.Identity.Name));
            List<AvailabilityRequestListModel> models = commands.Select(command =>
                RequestModelConverter.AvailabilityRequestListCommandConvertAvailabilityRequestListModel(command)).ToList();
            if (models == null)
                return null;
            return models;
        }

        [Authorize]
        [HttpGet("times/{id}")]
        public async Task<List<AvailableTimeModel>> GetAvailableTimesByAdvertId(int id)
        {
            List<AvailableTimeCommand> commands = await _requestService.GetAvailableTimesByAdvertId(id, await _accountService.GetIdByEmail(HttpContext.User.Identity.Name));
            List<AvailableTimeModel> models = commands.Select(command => RequestModelConverter.CommandConvertToAvailableTimeModel(command)).ToList();
            if (models == null)
                return null;
            return models;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create(AvailabilityRequestModelCreate model)
        {
            try
            {
                if (model == null)
                    return Ok("error");
                model.UserId = await _accountService.GetIdByEmail(HttpContext.User.Identity.Name);
                if(await _requestService.Create(RequestModelConverter.AvailabilityRequestModelCreateConvertCommand(model)))
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
        [HttpPut("confirm")]
        public async Task<IActionResult> Confirm(int id, int stateId)
        {
            try
            {
                if(await _requestService.Confirm(id, stateId))
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
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                if(await _requestService.Remove(id))
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
