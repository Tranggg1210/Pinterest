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

        [HttpGet("getUsersWithRole")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetUsersWithRole()
        {
            try
            {
                return Ok(await _repo.GetUsersWithRole());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
        [HttpPost("editRoles/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PostModel>> EditRoles(int id, List<string> roles)
        {
            try
            {
                var newRoles = await _repo.EditRoles(id, roles);
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
