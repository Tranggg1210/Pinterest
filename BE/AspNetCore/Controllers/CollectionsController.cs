using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PixelPalette.Entities;
using PixelPalette.Helpers;
using PixelPalette.Interfaces;
using PixelPalette.Models;
using PixelPalette.Repositories;

namespace PixelPalette.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionRepository _repo;
        private readonly UserManager<User> _userManager;

        public CollectionsController(ICollectionRepository repo, UserManager<User> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        [HttpGet("getAll")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<IEnumerable<CollectionModel>>> GetAll()
        {
            try
            {
                return Ok(await _repo.GetAllCollectionAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("getById/{id}")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<CollectionModel>> GetById(int id)
        {
            try
            {
                var collection = await _repo.GetCollectionByIdAsync(id);
                return collection == null ? NotFound("Not found") : Ok(collection);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("getByUserId")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<IEnumerable<CollectionModel>>> GetByUserId()
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                return Ok(await _repo.GetCollectionByUserIdAsync(user.Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("getByPostId/{postId}")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetByPostId(int postId)
        {
            try
            {
                return Ok(await _repo.GetCollectionByPostIdAsync(postId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("checkOwnCollection/{postId}")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<bool>> CheckOwnCollection(int postId)
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                return Ok(await _repo.CheckOwnCollectionAsync(postId, user.Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("create")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<CollectionModel>> Create(CollectCreateParams entryParams)
        {
            try
            {
                string userName = _userManager.GetUserName(HttpContext.User);
                var user = await _userManager.FindByNameAsync(userName);
                var collection = await _repo.AddCollectionAsync(user.Id, entryParams);
                return Ok(collection);
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
                var result = await _repo.DeleteCollectionAsync(id);
                return !result ? NotFound("Not found") : Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult<CollectionModel>> Update(int id, CollectUpdateParams entryParams)
        {
            try
            {
                var collection = await _repo.UpdateCollectionAsync(id, entryParams);
                return collection == null ? NotFound("Not found") : Ok(collection);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("background/{id}")]
        [Authorize(Roles = "Member")]
        public async Task<ActionResult> Background(int id, IFormFile file)
        {
            try
            {
                var backgroundUrl = await _repo.EditBackgroundAsync(id, file);
                if (string.IsNullOrEmpty(backgroundUrl))
                    return BadRequest(false);
                return Ok(backgroundUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
