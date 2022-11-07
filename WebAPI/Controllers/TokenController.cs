using Microsoft.AspNetCore.Mvc;
using Application.IServices;
using Application;
using System.Threading.Tasks;
using WebAPI.Models;
using System.Security.Claims;
using System;
using Application.Commands;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IAccountService _accountService;
        private ITokenService _tokenService;

        public TokenController(IUnitOfWork unitOfWork, IAccountService accountService, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(TokenModel tokenModel)
        {
            try
            {
                if (tokenModel is null)
                    return BadRequest("Invalid client request");
                string accessToken = tokenModel.AccessToken;
                string refreshToken = tokenModel.RefreshToken;
                ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
                string email = principal.Identity.Name;
                UserTokenCommand user = await _accountService.GetUserTokenByLogin(email);
                if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                    return BadRequest("Invalid client request");
                string newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
                string newRefreshToken = _tokenService.GenerateRefreshToken();
                await _accountService.RefreshUserToken(user.Id, newRefreshToken);
                await _unitOfWork.Commit();
                return Ok(new AuthenticatedResponse()
                {
                    Token = newAccessToken,
                    RefreshToken = newRefreshToken
                });
            }
            catch
            {
                return BadRequest("error");
            }
        }

        [Authorize]
        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke()
        {
            var userLogin = User.Identity.Name;
            UserTokenCommand user = await _accountService.GetUserTokenByLogin(userLogin);
            if (user == null)
                return BadRequest("error");
            await _accountService.RefreshUserToken(user.Id, null);
            await _unitOfWork.Commit();
            return NoContent();
        }
    }
}
