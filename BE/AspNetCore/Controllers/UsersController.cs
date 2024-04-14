using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PixelPalette.Entities;
using PixelPalette.Helpers;
using PixelPalette.Interfaces;

namespace PixelPalette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UsersController(IUserRepository repo)
        {
            _userRepo = repo;
        }

        [HttpGet("getUsers")]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _userRepo.GetAllUsersAsync());
            }
            catch
            {
                return BadRequest("An error when get list user!");
            }
        }
        [HttpGet("getUser/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            return user == null ? NotFound($"Can't find user by id is {id}!") : Ok(user);
        }

        [HttpDelete("deleteUser/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _userRepo.DeleteUserAsync(id);
                return result ? NotFound($"Can't find user by id is {id}!") : Ok("Remove user successful!");
            }
            catch
            {
                return BadRequest("An error when remove user!");
            }
        }

        [HttpPut("EditAvatar/{id}")]
        [Authorize]
        public async Task<IActionResult> EditUserAvatar(int id, IFormFile file)
        {
            var avatarUrl = await _userRepo.EditAvatar(id, file);
            if (!string.IsNullOrEmpty(avatarUrl))
                return Ok(avatarUrl);
            return BadRequest("An error when edit avatar!");
        }

        [HttpPut("EditProfile/{id}")]
        [Authorize]
        public async Task<IActionResult> EditProfile(int id, ProfileParams param)
        {
            try
            {
                var profile = await _userRepo.UpdateProfileAsync(id, param);
                return profile == null ? NotFound($"Can't find profile info by id is {id}!") : Ok(profile);
            }
            catch
            {
                return BadRequest("An error when edit profile info!");
            }
        }

        [HttpPut("EditAccount/{id}")]
        [Authorize]
        public async Task<IActionResult> EditAccount(int id, AccountParams param)
        {
            try
            {
                var account = await _userRepo.UpdateAccountAsync(id, param);
                return account == null ? NotFound($"Can't find account by id is {id}!") : Ok(account);
            }
            catch
            {
                return BadRequest("An error when edit account info!");
            }
        }
    }
}
