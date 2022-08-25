using Application;
using Microsoft.AspNetCore.Mvc;
using Application.IServices;
using WebAPI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Application.Commands;
using WebAPI.ModelsConverters;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IAccountService _accountService;
        
        public AccountController(IUnitOfWork unitOfWork, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (IsUserAuthorized().Name != null)
                    {
                        return Ok("authorize");
                    }
                    model.Password = _accountService.HashPassword(model.Password);
                    bool result = await _accountService.GetLoginResult(model.Email, model.Password);
                    if (result)
                    {
                        await Authenticate(model.Email);
                        return Ok("success");
                    }
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
                return Ok("error");
            }
            catch
            {
                return BadRequest("error");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (IsUserAuthorized().Name != null)
                    {
                        return Ok("authorize");
                    }
                    if (await _accountService.GetRegisterResult(model.Email))
                    {
                        model.Password = _accountService.HashPassword(model.Password);
                        UserCreateCommand userCreateCommand = UserModelConverter.RegisterModelConvertToUserCreateCommand(model);
                        if (await _accountService.Create(userCreateCommand))
                        {
                            await _unitOfWork.Commit();
                            await Authenticate(model.Email);
                            return Ok("success");
                        }
                        return Ok("error");
                    }
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
                return Ok("error");
            }
            catch
            {
                return BadRequest("error");
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                if (IsUserAuthorized().Name == null)
                {
                    return Ok("error");
                }
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok("success");
            }
            catch
            {
                return BadRequest("error");
            }
        }

        private async Task Authenticate(string userEmail)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userEmail),
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpGet("is-authorized")]
        public AuthoriseModel IsUserAuthorized()
        {
            AuthoriseModel authorise = new AuthoriseModel(HttpContext.User.Identity.Name);
            return authorise;
        }

        [Authorize]
        [HttpGet("by-id/{id}")]
        public async Task<UserModel> GetById(int id)
        {
            UserCommand userCommand = await _accountService.GetById(id);
            UserModel userModel = UserModelConverter.UserCommandConvertToUserModel(userCommand);
            if (userModel == null)
                return null;
            return userModel;
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update(UserUpdateModel user)
        {
            try
            {
                UserModel getUser = UserModelConverter.UserCommandConvertToUserModel(await _accountService.GetById(user.Id));
                if (getUser == null)
                {
                    return Ok("error");
                }
                if (!await _accountService.GetRegisterResult(user.Email) && getUser.Email != user.Email)
                {
                    return Ok("error");
                }
                user.Password = _accountService.HashPassword(user.Password);
                UserUpdateCommand userCommand = UserModelConverter.UserUpdateModelConvertToUserUpdateCommand(user);
                if (await _accountService.Update(userCommand))
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
                if (await _accountService.Remove(id))
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
