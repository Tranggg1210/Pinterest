using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PixelPalette.Interfaces;
using PixelPalette.Models;

namespace PixelPalette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UsersController(IUserRepository repo)
        {
            _userRepo = repo;
        }
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _userRepo.GetAllUsersAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepo.GetUserByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddUser(UserModel model)
        {
            try
            {
                var newUserId = await _userRepo.AddUserAsync(model);
                var newUser = await _userRepo.GetUserByIdAsync(newUserId);
                return newUser == null ? NotFound() : Ok(newUser);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateUser(int id, UserModel model)
        {
            try
            {
                await _userRepo.UpdateUserAsync(id, model);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        //[Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userRepo.DeleteUserAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
