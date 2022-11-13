using Microsoft.AspNetCore.Mvc;
using Application.IServices;
using WebAPI.ModelsConverters;
using Application;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using WebAPI.Models;
using System.Collections.Generic;
using Application.Commands;
using System.Linq;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/review")]
    public class ReviewController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IReviewService _reviewService;
        private IAccountService _accountService;

        public ReviewController(IReviewService reviewService, IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _reviewService = reviewService;
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        [Authorize]
        [HttpGet("user/{count}")]
        public async Task<List<ReviewModel>> GetByUserId(int count)
        {
            List<ReviewCommand> commands = await _reviewService.GetByUserId(await _accountService.GetIdByEmail(User.Identity.Name), count);
            List<ReviewModel> models = commands.Select(command => ReviewModelConverter.ReviewCommandCovertToModel(command)).ToList();
            string name = "";
            UserCommand user = new UserCommand();
            for (int index = 0; index < models.Count; index++)
            {
                name = await _accountService.GetUserNameById(commands[index].UserId);
                models[index].Name = name;
                user = await _accountService.GetUserByEmail(User.Identity.Name);
                if (name == user.Name)
                    models[index].IsAuthorized = true;
                else
                    models[index].IsAuthorized = false;
            }
            if (models == null)
                return null;
            return models;
        }

        [HttpGet("advert/{id}/{count}")]
        public async Task<List<ReviewModel>> GetByAdvertId(int id, int count)
        {
            List<ReviewCommand> commands = await _reviewService.GetByAdvertId(id, count);
            List<ReviewModel> models = commands.Select(command => ReviewModelConverter.ReviewCommandCovertToModel(command)).ToList();
            string name = "";
            UserCommand user = new UserCommand();
            for (int index = 0; index < models.Count; index++)
            {
                name = await _accountService.GetUserNameById(commands[index].UserId);
                models[index].Name = name;
                user = await _accountService.GetUserByEmail(User.Identity.Name);
                if(user != null)
                    if (name == user.Name)
                        models[index].IsAuthorized = true;
                    else
                        models[index].IsAuthorized = false;
                else
                    models[index].IsAuthorized = false;
            }
            if (models == null)
                return null;
            return models;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ReviewModelInfo> GetById(int id)
        {
            ReviewCommand command = await _reviewService.GetById(id);
            if (command.UserId != await _accountService.GetIdByEmail(User.Identity.Name))
                return null;
            ReviewModelInfo model = ReviewModelConverter.ReviewCommandCovertToModelInfo(command);
            if (model == null)
                return null;
            return model;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("error");
                if (ReviewModelConverter.ReviewCommandCovertToModel(await _reviewService.GetById(await _accountService.GetIdByEmail(User.Identity.Name))).Id != id)
                    return BadRequest("error");
                if (await _reviewService.Remove(id))
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
        [HttpPost("create")]
        public async Task<IActionResult> Create(ReviewModelCreate review)
        {
            try
            {
                if(review == null)
                    return BadRequest("error");
                review.UserId = await _accountService.GetIdByEmail(User.Identity.Name);
                if(await _reviewService.Create(ReviewModelConverter.ReviewModelCreateConvertToCommand(review)))
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
        [HttpPut("update")]
        public async Task<IActionResult> Update(ReviewModelUpdate review)
        {
            try
            {
                if (review == null)
                    return BadRequest("error");
                review.UserId = await _accountService.GetIdByEmail(User.Identity.Name);
                if(await _reviewService.Update(ReviewModelConverter.ReviewModelUpdateConvertToCommand(review)))
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
    }
}
