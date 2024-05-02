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
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _repo;
        private readonly UserManager<User> _userManager;
        private Thumbnail? _thumbnail = null;

        public PostsController(IPostRepository repo, UserManager<User> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }
        [HttpGet("getAll")]
        [Authorize]
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
        [Authorize]
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

        [HttpGet("getByCollectionId/{collectionId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetByCollectionId(int collectionId)
        {
            try
            {
                return Ok(await _repo.GetPostByCollectionIdAsync(collectionId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("getByUserId/{userId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetByUserId()
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                return Ok(await _repo.GetPostByUserIdAsync(user.Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<PostModel>> Create(PostCreateParams entryParams)
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                var post = await _repo.AddPostAsync(user.Id, entryParams);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("toggleCollection")]
        [Authorize]
        public async Task<ActionResult> ToggleCollection(int postId, int collectionId)
        {
            try
            {
                var result = await _repo.OwnershipAsync(postId, collectionId);
                return !result ? NotFound("Not found") : Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("toggleLike")]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult<PostModel>> Update(PostUpdateParams entryParams)
        {
            try
            {
                if (_thumbnail != null)
                {
                    string userName = _userManager.GetUserName(HttpContext.User);
                    var user = await _userManager.FindByNameAsync(userName);
                    var post = await _repo.UpdatePostAsync(user.Id, entryParams, _thumbnail);
                    if (post != null) return Ok(post);
                }
                return BadRequest(false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("upload")]
        [Authorize]
        public async Task<ActionResult<Thumbnail>> Upload(IFormFile file)
        {
            var result = await _repo.UploadThumbnailAsync(file);
            if (result != null)
            {
                _thumbnail = new Thumbnail
                {
                    PublicId = result.PublicId,
                    Url = result.SecureUrl.AbsoluteUri
                };
                return Ok(_thumbnail);
            }
            return BadRequest(false);
        }

        [HttpDelete("cancel")]
        [Authorize]
        public async Task<ActionResult> Cancel(string publicId)
        {
            var result = await _repo.DeleteThumbnailAsync(publicId);
            return !result ? BadRequest(false) : Ok(true);
        }

    }
}
