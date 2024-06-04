using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class AnalysisesController : ControllerBase
    {
        private readonly IAnalysisesRepository _repo;
        private readonly UserManager<User> _userManager;

        public AnalysisesController(IAnalysisesRepository repo, UserManager<User> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        [HttpGet("getAnalysisToday")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AnalysisModel>> GetAnalysisToday()
        {
            try
            {
                return Ok(await _repo.GetAnalysisTodayAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("getNotifications")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<AnalysisModel>> GetNotifications()
        {
            try
            {
                return Ok(await _repo.GetNotificationsAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete("deleteNotification/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteNotification(int id)
        {
            try
            {
                var result = await _repo.DeleteNotificationAsync(id);
                return !result ? NotFound("Not found") : Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("createNotification")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateNotification(NotificationParam entryParams)
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                return Ok(await _repo.AddNotificationAsync(user.Id, entryParams));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
