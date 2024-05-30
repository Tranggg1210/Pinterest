using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using PixelPalette.Entities;
using PixelPalette.Helpers;
using PixelPalette.Interfaces;
using PixelPalette.Models;

namespace PixelPalette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _repo;
        private readonly UserManager<User> _userManager;

        public PostsController(IPostRepository repo, UserManager<User> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }
        [HttpGet("getAll")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetAll()
        {
            try
            {
                return Ok(await _repo.GetAllPostAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
        [HttpGet("getById/{id}")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<PostModel>> GetById(int id)
        {
            try
            {
                var post = await _repo.GetPostByIdAsync(id);
                return post == null ? NotFound("Not found") : Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("getByCollectionId")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetByCollectionId(int? collectionId)
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                var collections = await _repo.GetPostByCollectionIdAsync(user.Id, collectionId);
                return collections != null ? Ok(collections) : Ok(Enumerable.Empty<PostModel>());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("getByUserId")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetByUserId(int? userId)
        {
            try
            {
                if (userId == null)
                {
                    string userName = _userManager.GetUserName(HttpContext.User);
                    var user = await _userManager.FindByNameAsync(userName);
                    userId = user.Id;
                }
                return Ok(await _repo.GetPostByUserIdAsync((int)userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("create")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<PostModel>> Create([FromForm] PostCreateParams entryParams, IFormFile file)
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                var post = await _repo.AddPostAsync(user.Id, entryParams, file);
                if (post != null) return Ok(post);
                return BadRequest(false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("toggleCollection")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult> ToggleCollection(int postId, int? collectionId)
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                var result = await _repo.OwnershipAsync(user.Id, postId, collectionId);
                return !result ? NotFound("Not found") : Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("toggleLike")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult> ToggleLike(int postId)
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                var result = await _repo.LikePostAsync(postId, user.Id);
                return !result ? NotFound("Not found") : Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _repo.DeletePostAsync(id);
                return !result ? NotFound("Not found") : Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<PostModel>> Update(int id, [FromForm] PostUpdateParams entryParams, IFormFile? file)
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                var post = await _repo.UpdatePostAsync(id, entryParams, user.Id, file);
                if (post != null) return Ok(post);
                return BadRequest(false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("checkLike/{postId}")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<bool>> CheckLike(int postId)
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                return Ok(await _repo.checkLikeAsync(postId, user.Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
