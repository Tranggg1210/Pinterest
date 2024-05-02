using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using PixelPalette.Data;
using PixelPalette.Entities;
using PixelPalette.Helpers;
using PixelPalette.Interfaces;
using PixelPalette.Models;

namespace PixelPalette.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly PixelPaletteContext _context;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly ITool _tools;

        public PostRepository(PixelPaletteContext context, IMapper mapper, IPhotoService photoService, ITool tools)
        {
            _context = context;
            _mapper = mapper;
            _photoService = photoService;
            _tools = tools;
        }
        public async Task<PostModel> AddPostAsync(int userId, PostCreateParams entryParams)
        {
            var model = new PostModel();
            model.UserId = userId;
            _tools.Duplicate(entryParams, ref model);
            var post = _mapper.Map<Post>(model);
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return _mapper.Map<PostModel>(post);
        }

        public async Task<bool> OwnershipAsync(int postId, int collectionId)
        {
            var ownership = await _context.Ownerships
                .FirstOrDefaultAsync(o => o.PostId == postId && o.CollectionId == collectionId);
            if (ownership != null)
            {
                _context.Ownerships.Remove(ownership);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                var post = await _context.Posts.FindAsync(postId);
                var collection = await _context.Collections.FindAsync(collectionId);
                if (post != null && collection != null)
                {
                    ownership = new Ownership
                    {
                        PostId = postId,
                        CollectionId = collectionId
                    };
                    await _context.AddAsync(ownership);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var deletePost = _context.Posts!.SingleOrDefault(p => p.Id == id);
            if (deletePost != null)
            {
                var deleteResult = await _photoService.DeletePhotoAsync(deletePost.ThumbnailId);
                if (deleteResult.Error != null || deleteResult.Result == "not found") return false;

                _context.Posts!.Remove(deletePost);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteThumbnailAsync(string publicId)
        {
            var deleteResult = await _photoService.DeletePhotoAsync(publicId);
            if (deleteResult.Error != null || deleteResult.Result == "not found")
                return false;
            return true;
        }

        public async Task<IEnumerable<PostModel>> GetAllPostAsync()
        {
            var posts = await _context.Posts!.ToListAsync();
            return _mapper.Map<IEnumerable<PostModel>>(posts);
        }

        public async Task<PostModel> GetPostByCollectionIdAsync(int collectionId)
        {
            var posts = await _context.Ownerships!
                .Where(o => o.CollectionId == collectionId)
                .Join(_context.Posts, o => o.PostId, p => p.Id, (o, p) => p)
                .ToListAsync();
            return _mapper.Map<PostModel>(posts);
        }

        public async Task<PostModel> GetPostByIdAsync(int id)
        {
            var post = await _context.Posts!.FindAsync(id);
            return _mapper.Map<PostModel>(post);
        }

        public async Task<PostModel> GetPostByUserIdAsync(int userId)
        {
            var posts = await _context.Posts
                .Where(p => p.UserId == userId)
                .ToListAsync();
            return _mapper.Map<PostModel>(posts);
        }

        public async Task<PostModel> UpdatePostAsync(int id, PostUpdateParams entryParams, Thumbnail thumbnail)
        {
            var updatePost = await _context.Posts!.FindAsync(id);
            if (updatePost != null)
            {
                var deleteResult = await _photoService.DeletePhotoAsync(updatePost.ThumbnailId);
                if (deleteResult.Error != null) return null!;

                _tools.Duplicate(entryParams, ref updatePost);
                _context.Posts!.Update(updatePost);
                await _context.SaveChangesAsync();

                return _mapper.Map<PostModel>(updatePost);
            }
            return null!;
        }

        public async Task<ImageUploadResult> UploadThumbnailAsync(IFormFile file)
        {
            var addResult = await _photoService.AddPhotoAsync(file);
            if (addResult.Error != null) return null!;
            return addResult;
        }

        public async Task<bool> LikePostAsync(int postId, int userId)
        {
            var likePost = await _context.LikePosts
                .FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);
            var post = await _context.Posts.FindAsync(postId);
            if (likePost != null)
            {
                _context.LikePosts.Remove(likePost);
                if (post!.Like > 0) post!.Like--;
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                var user = await _context.Users.FindAsync(userId);
                if (post != null && user != null)
                {
                    likePost = new LikePost
                    {
                        PostId = postId,
                        UserId = userId
                    };
                    await _context.LikePosts.AddAsync(likePost);
                    if (post!.Like > 0) post!.Like++;
                    _context.Posts.Update(post);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
    }
}
