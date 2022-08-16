using Microsoft.AspNetCore.Mvc;
using Application.Commands;
using Application.IServices;
using Application;
using WebAPI.Models;
using WebAPI.ModelsConverters;
using Microsoft.AspNetCore.Authorization;

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
        [HttpGet("for-customer")]
        public async Task<List<AvailabilityRequestModelForCustomer>> GetForCustomer()
        {
            List<AvailabilityRequestCommandForCustomer> commands = await _requestService.GetForCustomer(await _accountService.GetByEmail(HttpContext.User.Identity.Name));
            List<AvailabilityRequestModelForCustomer> models = commands.Select(command => RequestModelConverter.AvailabilityRequestForCustomerCommandConvertModel(command)).ToList();
            if (models == null)
                return null;
            return models;
        }

        [Authorize]
        [HttpGet("for-landlord")]
        public async Task<List<AvailabilityRequestModelForLandlord>> GetForLandlord()
        {
            List<AvailabilityRequestCommandForLandlord> commadns = await _requestService.GetForLandlord(await _accountService.GetByEmail(HttpContext.User.Identity.Name));
            List<AvailabilityRequestModelForLandlord> models = commadns.Select(command => RequestModelConverter.AvailabilityRequestCommandForLandlordConvertModel(command)).ToList();
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
                if(await _requestService.Create(RequestModelConverter.availabilityRequestModelCreateConvertCommand(model)))
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
                    _unitOfWork.Commit();
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
                    _unitOfWork.Commit();
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
