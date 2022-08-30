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
                model.RequestStateId = 3;
                if(await _requestService.Create(RequestModelConverter.AvailabilityRequestModelCreateConvertCommand(model)))
                {
                    await _unitOfWork.Commit();
                    int requestId = await _requestService.GetLastRequestId();
                    if (requestId == 0)
                        return Ok("error");
                    List<AvailableTimeCommandForCreateRequest> times = model.AvailableTimeModelForCreateRequests.Select(time => RequestModelConverter.AvailableTimeModelForCreateRequestConvertToCommand(time)).ToList();
                    await _requestService.UpdateTimes(requestId, times);
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
        public async Task<IActionResult> Confirm(ConfirmModel model)
        {
            try
            {
                if (model.RequestStateId != 1 && model.RequestStateId != 2 && model.RequestStateId != 3)
                    return Ok("error");
                if (await _requestService.Confirm(model.Id, model.RequestStateId))
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
                int userId = await _accountService.GetIdByEmail(HttpContext.User.Identity.Name);
                if (await _requestService.Remove(id, userId))
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
