using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PixelPalette.Interfaces;
using PixelPalette.Models;

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
                return BadRequest("Can't get list user successful!");
            }
        }
        [HttpGet("getUser/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            return user == null ? NotFound($"Can't find user by id is {id}!") : Ok(user);
        }

        [HttpPut("updateUser/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(int id, UserModel model)
        {
            try
            {
                await _userRepo.UpdateUserAsync(id, model);
                return Ok("Update user successful!");
            }
            catch
            {
                return BadRequest("Update user failure!");
            }
        }
        [HttpDelete("deleteUser/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userRepo.DeleteUserAsync(id);
                return Ok("Remove user successful!");
            }
            catch
            {
                return BadRequest("Remove user failure!");
            }
        }
        [HttpPut("EditAvatar/{id}")]
        [Authorize]
        public async Task<IActionResult> EditUserAvatar(int id, IFormFile file)
        {
            try
            {
                await _userRepo.EditAvatar(id, file);
                return Ok("Edit profile picture user successful!");
            }
            catch
            {
                return BadRequest("Edit profile picture user failure!");
            }
        }

        [HttpPut("EditProfile/{id}")]
        [Authorize]
        public async Task<IActionResult> EditProfile(int id, ProfileModel profileModel)
        {
            try
            {
                await _userRepo.UpdateProfileAsync(id, profileModel);
                return Ok("Edit profile info user successful!");
            }
            catch
            {
                return BadRequest("Edit profile info user failure!");
            }
        }

        [HttpPut("EditAccount/{id}")]
        [Authorize]
        public async Task<IActionResult> EditAccount(int id, AccountModel accountModel)
        {
            try
            {
                await _userRepo.UpdateAccountAsync(id, accountModel);
                return Ok("Edit account info user successful!");
            }
            catch
            {
                return BadRequest("Edit account info user failure!");
            }
        }
    }
}
