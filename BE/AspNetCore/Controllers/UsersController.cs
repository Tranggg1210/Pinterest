using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PixelPalette.Entities;
using PixelPalette.Helpers;
using PixelPalette.Interfaces;
using PixelPalette.Models;

namespace PixelPalette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly UserManager<User> _userManager;

        public UsersController(IUserRepository repo, UserManager<User> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        [HttpGet("getAll")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAll()
        {
            try
            {
                return Ok(await _repo.GetAllUsersAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
        [HttpGet("getById/{id}")]
        [Authorize]
        public async Task<ActionResult<UserModel>> GetUser(int id)
        {
            try
            {
                var user = await _repo.GetUserByIdAsync(id);
                return user == null ? NotFound("Not found") : Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("getLoginUser")]
        [Authorize]
        public async Task<ActionResult<UserModel>> GetLoginUser()
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                var loginer = await _repo.GetUserByIdAsync(user.Id);
                return loginer == null ? NotFound("Not found") : Ok(loginer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("follower/{followingId}")]
        [Authorize]
        public async Task<ActionResult> Follower(int followingId)
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                var result = await _repo.FollowHandleAsync(user.Id, followingId);
                return !result ? NotFound("Not found") : Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("unfollower/{followingId}")]
        [Authorize]
        public async Task<ActionResult> Unfollower(int followingId)
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                var result = await _repo.UnfollowHandleAsync(user.Id, followingId);
                return !result ? NotFound("Not found") : Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _repo.DeleteUserAsync(id);
                return result ? NotFound("Not found") : Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPut("avatar/{id}")]
        [Authorize]
        public async Task<ActionResult> Avatar(IFormFile file)
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                var avatarUrl = await _repo.EditAvatarAsync(user.Id, file);
                if (string.IsNullOrEmpty(avatarUrl))
                    return BadRequest(false);
                return Ok(avatarUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPut("profile/{id}")]
        [Authorize]
        public async Task<ActionResult<UserModel>> Profile(ProfileParams entryParams)
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                var profile = await _repo.UpdateProfileAsync(user.Id, entryParams);
                return profile == null ? NotFound("Not found") : Ok(profile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPut("account/{id}")]
        [Authorize]
        public async Task<ActionResult<UserModel>> Account(AccountParams entryParams)
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                var account = await _repo.UpdateAccountAsync(user.Id, entryParams);
                return account == null ? NotFound("Not found") : Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
