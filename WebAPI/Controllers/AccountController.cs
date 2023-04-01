using Application;
using Microsoft.AspNetCore.Mvc;
using Application.IServices;
using WebAPI.Models;
using System.Security.Claims;
using Application.Commands;
using WebAPI.ModelsConverters;
using Microsoft.AspNetCore.Authorization;
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
        private IRequestService _requestService;

        public AccountController(IUnitOfWork unitOfWork, IAccountService accountService, ITokenService tokenService, IRequestService requestService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
            _tokenService = tokenService;
            _requestService = requestService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (model is null)
                    return BadRequest("error");
                model.Password = _accountService.HashPassword(model.Password);
                if (!await _accountService.GetLoginResult(model.Email, model.Password))
                    return BadRequest("error");
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
                if (!await _accountService.GetRegisterResult(model.Email))
                    return BadRequest("error");
                model.Password = _accountService.HashPassword(model.Password);
                UserCreateCommand userCreateCommand = UserModelConverter.RegisterModelConvertToUserCreateCommand(model);
                if (!await _accountService.Create(userCreateCommand))
                    return BadRequest("error");
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
            UserCommand user = await _accountService.GetUserByEmail(User.Identity.Name);
            if (user == null)
                return null;
            AuthorizeModel authorise = new AuthorizeModel(user.Name, user.Email, await _requestService.IsAttention(user.Id));
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
        [HttpGet("user/{id}")]
        public async Task<UserModel> GetUserById(int id)
        {
            UserCommand userCommand = await _accountService.GetById(id);
            UserModel userModel = UserModelConverter.UserCommandConvertToUserModel(userCommand);
            if (userModel == null)
                return null;
            return userModel;
        }

        [HttpGet("user-profile/{id}")]
        public async Task<UserModel> GetProfileById(int id)
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
                    return BadRequest("error");
                if (!await _accountService.GetRegisterResult(user.Email) && getUser.Email != user.Email)
                    return BadRequest("error");
                user.Password = _accountService.HashPassword(user.Password);
                UserUpdateCommand userCommand = UserModelConverter.UserUpdateModelConvertToUserUpdateCommand(user);
                if (!await _accountService.Update(userCommand))
                    return BadRequest("error");
                await _unitOfWork.Commit();
                int accountId = await _accountService.GetIdByEmail(userCommand.Email);
                List<Claim> identity = GetIdentity(userCommand.Email);
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
                if (!await _accountService.Remove(id))
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
