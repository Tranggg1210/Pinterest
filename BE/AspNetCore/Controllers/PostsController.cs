using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PixelPalette.Helpers;
using PixelPalette.Interfaces;

namespace PixelPalette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepo;

        public PostsController(IPostRepository repo)
        {
            _postRepo = repo;
        }
        [HttpGet("getPosts")]
        [Authorize]
        public async Task<IActionResult> GetAllPosts()
        {
            try
            {
                return Ok(await _postRepo.GetAllPostAsync());
            }
            catch
            {
                return BadRequest("An error when get list post!");
            }
        }
        [HttpGet("getPost/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUser(int id)
        {
            var post = await _postRepo.GetPostByIdAsync(id);
            return post == null ? NotFound($"Can't find post by id is {id}!") : Ok(post);
        }

        [HttpDelete("deletePost/{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                var result = await _postRepo.DeletePostAsync(id);
                return !result ? NotFound($"Can't find post by id is {id}!") : Ok("Remove post successful!");
            }
            catch
            {
                return BadRequest("An error when remove post!");
            }
        }

        [HttpPost("createPost")]
        [Authorize]
        public async Task<IActionResult> CreatePost(PostInitParams param)
        {
            try
            {
                var post = await _postRepo.AddPostAsync(param);
                return Ok(post);
            }
            catch
            {
                return BadRequest("An error when add new post!");
            }
        }

        [HttpPut("updatePost/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePost(int id, PostSetParams param)
        {
            try
            {
                var post = await _postRepo.UpdatePostAsync(id, param);
                return post == null ? NotFound($"Can't find post by id is {id}!") : Ok(post);
            }
            catch
            {
                return BadRequest("An error when edit post!");
            }
        }

        [HttpPost("uploadThumbnail")]
        [Authorize]
        public async Task<IActionResult> UploadThumbnail(IFormFile file)
        {
            var result = await _postRepo.UploadThumbnailAsync(file);
            return result == null ? BadRequest("An error when upload image to cloud!") :
                Ok(new
                {
                    ThumbnailUrl = result.SecureUrl.AbsoluteUri,
                    ThumbnailId = result.PublicId
                });
        }

        [HttpDelete("deleteThumbnail")]
        [Authorize]
        public async Task<IActionResult> DeleteThumbnail(string publicId)
        {
            try
            {
                var result = await _postRepo.DeleteThumbnailAsync(publicId);
                return result ? NotFound($"Can't find thumbnail by id is {publicId}!") : Ok("Remove thumbnail successful!");
            }
            catch (Exception)
            {
                return BadRequest("An error when delete or not found image on cloud!");
            }
        }

    }
}
