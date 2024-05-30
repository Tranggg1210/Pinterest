using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PixelPalette.Helpers;
using PixelPalette.Interfaces;
using PixelPalette.Models;

namespace PixelPalette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _repo;
        public AdminController(IAdminRepository repo)
        {
           _repo = repo;
        }

        [HttpGet("GetAllRoles")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAllRoles()
        {
            try
            {
                return Ok(await _repo.GetAllRolesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("GetRole/{UserId}")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult> GetRole(int UserId)
        {
            try
            {
                return Ok(await _repo.GetRoleByUserIdAsync(UserId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("EditRole/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PostModel>> EditRole(int id, List<string> roles)
        {
            try
            {
                var newRoles = await _repo.EditRoleAsync(id, roles);
                if (newRoles != null) return Ok(newRoles);
                return BadRequest(false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

    }
}
