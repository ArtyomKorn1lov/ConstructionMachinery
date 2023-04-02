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
            AvailabilityRequestCommandForCustomer command = await _requestService.GetForCustomer(id, await _accountService.GetIdByEmail(User.Identity.Name));
            AvailabilityRequestModelForCustomer model = RequestModelConverter.AvailabilityRequestForCustomerCommandConvertModel(command);
            if (model == null)
                return null;
            return model;
        }

        [Authorize]
        [HttpGet("landlord/{id}")]
        public async Task<AvailabilityRequestModelForLandlord> GetForLandlord(int id)
        {
            AvailabilityRequestCommandForLandlord command = await _requestService.GetForLandlord(id, await _accountService.GetIdByEmail(User.Identity.Name));
            AvailabilityRequestModelForLandlord model = RequestModelConverter.AvailabilityRequestCommandForLandlordConvertModel(command);
            if (model == null)
                return null;
            await _unitOfWork.Commit();
            return model;
        }

        [Authorize]
        [HttpGet("for-customer/{id}/{count}")]
        public async Task<List<AvailabilityRequestListModel>> GetListForCustomer(int id, int count)
        {
            List<AvailabilityRequestListCommand> commands = await _requestService.GetListForCustomer(id, await _accountService.GetIdByEmail(User.Identity.Name), count);
            List<AvailabilityRequestListModel> models = commands.Select(command =>
                RequestModelConverter.AvailabilityRequestListCommandConvertAvailabilityRequestListModel(command)).ToList();
            if (models == null)
                return null;
            return models;
        }

        [Authorize]
        [HttpGet("for-landlord/{count}")]
        public async Task<List<AvailabilityRequestListModel>> GetListForLandlord(int count)
        {
            List<AvailabilityRequestListCommand> commands = await _requestService.GetListForLandlord(await _accountService.GetIdByEmail(User.Identity.Name), count);
            List<AvailabilityRequestListModel> models = commands.Select(command =>
                RequestModelConverter.AvailabilityRequestListCommandConvertAvailabilityRequestListModel(command)).ToList();
            if (models == null)
                return null;
            return models;
        }

        [Authorize]
        [HttpGet("times/{id}")]
        public async Task<List<AvailableDayModel>> GetAvailableTimesByAdvertId(int id)
        {
            List<AvailiableDayCommand> commands = await _requestService.GetAvailableTimesByAdvertId(id, await _accountService.GetIdByEmail(User.Identity.Name));
            List<AvailableDayModel> models = commands.Select(command => RequestModelConverter.CommandConvertToAvailableDayModel(command)).ToList();
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
                    return BadRequest("error");
                model.UserId = await _accountService.GetIdByEmail(User.Identity.Name);
                model.RequestStateId = 3;
                if (!await _requestService.Create(RequestModelConverter.AvailabilityRequestModelCreateConvertCommand(model)))
                    return BadRequest("error");
                await _unitOfWork.Commit();
                int requestId = await _requestService.GetLastRequestId();
                if (requestId == 0)
                    return BadRequest("error");
                List<AvailableTimeCommandForCreateRequest> times = model.AvailableTimeModelForCreateRequests.Select(time => RequestModelConverter.AvailableTimeModelForCreateRequestConvertToCommand(time)).ToList();
                await _requestService.UpdateTimes(requestId, times);
                await _unitOfWork.Commit();
                return Ok("success");
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
                    return BadRequest("error");
                int currentUserId = await _accountService.GetIdByEmail(User.Identity.Name);
                if (!await _requestService.Confirm(model.Id, model.RequestStateId, currentUserId))
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
                int userId = await _accountService.GetIdByEmail(User.Identity.Name);
                if (!await _requestService.Remove(id, userId))
                    return BadRequest("error");
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
