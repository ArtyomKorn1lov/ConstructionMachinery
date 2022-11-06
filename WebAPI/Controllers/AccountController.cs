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
        private ITokenService _tokenService;
        public AccountController(IUnitOfWork unitOfWork, IAccountService accountService, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (model is null)
                    return BadRequest("error");
                if (await _accountService.GetLoginResult(model.Email, model.Password))
                {
                    model.Password = _accountService.HashPassword(model.Password);
                    int accountId = await _accountService.GetIdByEmail(model.Email);
                    List<Claim> identity = GetIdentity(model.Email);
                    string accessToken = _tokenService.GenerateAccessToken(identity);
                    string refreshToken = _tokenService.GenerateRefreshToken();
                    await _accountService.SetUserToken(accountId, refreshToken);
                    await _unitOfWork.Commit();
                    return Ok(new AuthenticatedResponse
                    {
                        Token = accessToken,
                        RefreshToken = refreshToken
                    });
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
                if (model is null)
                    return BadRequest("error");
                if (await _accountService.GetRegisterResult(model.Email))
                {
                    model.Password = _accountService.HashPassword(model.Password);
                    UserCreateCommand userCreateCommand = UserModelConverter.RegisterModelConvertToUserCreateCommand(model);
                    if (await _accountService.Create(userCreateCommand))
                    {
                        await _unitOfWork.Commit();
                        int accountId = await _accountService.GetIdByEmail(model.Email);
                        List<Claim> identity = GetIdentity(model.Email);
                        string accessToken = _tokenService.GenerateAccessToken(identity);
                        string refreshToken = _tokenService.GenerateRefreshToken();
                        await _accountService.SetUserToken(accountId, refreshToken);
                        await _unitOfWork.Commit();
                        return Ok(new AuthenticatedResponse
                        {
                            Token = accessToken,
                            RefreshToken = refreshToken
                        });
                    }
                    return Ok("error");
                }
                return Ok("error");
            }
            catch
            {
                return BadRequest("error");
            }
        }

        private List<Claim> GetIdentity(string email)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, email)
                };
            return claims;
        }

        [Authorize]
        [HttpGet("is-authorized")]
        public async Task<AuthorizeModel> IsUserAuthorized()
        {
            UserCommand user = await _accountService.GetUserByEmail(HttpContext.User.Identity.Name);
            if (user == null)
                return null;
            AuthorizeModel authorise = new AuthorizeModel(user.Name, user.Email);
            return authorise;
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<UserModel> GetById()
        {
            int id = await _accountService.GetIdByEmail(User.Identity.Name);
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
