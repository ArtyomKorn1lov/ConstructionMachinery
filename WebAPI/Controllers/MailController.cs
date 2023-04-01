using Application;
using Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Application.IServices;
using WebAPI.Models;
using WebAPI.ModelsConverters;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/mail")]
    public class MailController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IMailService _mailService;

        public MailController(IUnitOfWork unitOfWork, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _mailService = mailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMail(MailModelCreate model)
        {
            try
            {
                if (model == null)
                    return BadRequest("error");
                MailCommandCreate command = MailModelConverter.ModelConvertToMailCommandCreate(model);
                await _mailService.SaveMail(command);
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
