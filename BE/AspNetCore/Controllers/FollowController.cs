using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PixelPalette.Entities;
using PixelPalette.Interfaces;

namespace PixelPalette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowRepository _repo;
        private readonly UserManager<User> _userManager;

        public FollowController(IFollowRepository repo, UserManager<User> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }
        [HttpPost("follower/{followingId}")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult> Follower(int followingId)
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                var result = await _repo.FollowHandleAsync(user.Id, followingId);
                return !result ? Ok(false) : Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("unfollower/{followingId}")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult> Unfollower(int followingId)
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                var result = await _repo.UnfollowHandleAsync(user.Id, followingId);
                return !result ? Ok(false) : Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("checkFollower/{followingId}")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult> CheckFollower(int followingId)
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                var result = await _repo.CheckFollowAsync(user.Id, followingId);
                return !result ? Ok(false) : Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
