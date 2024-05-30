using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PixelPalette.Entities;
using PixelPalette.Helpers;
using PixelPalette.Interfaces;
using PixelPalette.Models;

namespace PixelPalette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _repo;
        private readonly UserManager<User> _userManager;

        public AccountsController(IAccountRepository repo, UserManager<User> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }
        [HttpPost("signUp")]
        public async Task<ActionResult> SignUp(SignUpModel signUpModel)
        {
            var result = await _repo.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                return Ok(true);
            }
            return BadRequest(false);
        }
        [HttpPost("signIn")]
        public async Task<ActionResult> SignIn(SignInModel signInModel)
        {
            if (!await _repo.SignInAsync(signInModel))
                return Unauthorized("Unauthorised");
            var token = await _repo.CreateToken();
            var cookieOptions = new CookieOptions
            {
                Path = "/",
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };
            Response.Cookies.Append("Token", token, cookieOptions);
            return Ok(new { token = token });
        }
        [HttpPut("changePassword")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult> ChangePassword(ChangePasswordParams entryParams)
        {
            string userName = _userManager.GetUserName(HttpContext.User);
            if (string.IsNullOrEmpty(userName) || !await _repo.ChangePasswordAsync(userName, entryParams))
                return BadRequest(false);
            return Ok(true);
        }
    }
}
