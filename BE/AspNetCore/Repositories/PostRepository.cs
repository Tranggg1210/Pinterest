using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
        public async Task<PostModel> AddPostAsync(int userId, PostCreateParams entryParams, IFormFile file)
        {
            var model = new PostModel();
            model.UserId = userId;
            var addResult = await _photoService.AddPhotoAsync(file);
            if (addResult.Error != null) return null!;
            model.ThumbnailId = addResult.PublicId;
            model.ThumbnailUrl = addResult.SecureUrl.AbsoluteUri;
            _tools.Duplicate(entryParams, ref model);
            var post = _mapper.Map<Post>(model);
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            if (entryParams.CollectionId != null)
                await OwnershipAsync(userId, post.Id, entryParams.CollectionId);

            return _mapper.Map<PostModel>(post);
        }

        public async Task<bool> OwnershipAsync(int userId, int postId, int? collectionId)
        {
            if (collectionId == null)
            {
                var collectionDefault = _context.Collections.FirstOrDefault(c => c.UserId == userId &&
                    c.Name.Equals("Bảng mặc định"));
                if (collectionDefault == null)
                {
                    var collectionNew = new Entities.Collection
                    {
                        UserId = userId,
                        Name = "Bảng mặc định",
                        IsDefault = true
                    };
                    _context.Collections.Add(collectionNew);
                    await _context.SaveChangesAsync();
                    collectionDefault = collectionNew;
                }
                collectionId = collectionDefault!.Id;
            }
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection != null)
            {
                var ownership = await _context.Ownerships
                    .FirstOrDefaultAsync(o => o.PostId == postId && o.CollectionId == collectionId);
                if (ownership != null)
                {
                    if (collection.PostCount > 0) collection.PostCount--;
                    _context.Ownerships.Remove(ownership);
                    _context.Collections.Update(collection);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    var post = await _context.Posts.FindAsync(postId);
                    if (post != null)
                    {
                        ownership = new Ownership
                        {
                            PostId = postId,
                            CollectionId = (int)collectionId!
                        };
                        collection.PostCount++;
                        await _context.AddAsync(ownership);
                        _context.Collections.Update(collection);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var deletePost = _context.Posts!.SingleOrDefault(p => p.Id == id);
            if (deletePost != null)
            {
                var collections = await _context.Ownerships!
                    .Where(o => o.PostId == id)
                    .Join(_context.Collections, o => o.CollectionId, c => c.Id, (o, c) => c)
                    .Select(r => new Entities.Collection
                    {
                        Id = r.Id,
                        UserId = r.UserId,
                        Name = r.Name,
                        Description = r.Description,
                        BackgroundId = r.BackgroundId,
                        BackgroundUrl = r.BackgroundUrl,
                        PostCount = r.PostCount - 1,
                        IsDefault = r.IsDefault
                    })
                    .ToListAsync();
                _context.Collections!.UpdateRange(collections);
                var deleteResult = await _photoService.DeletePhotoAsync(deletePost.ThumbnailId);
                if (deleteResult.Error != null || deleteResult.Result == "not found") return false;

                _context.Posts!.Remove(deletePost);
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<IEnumerable<PostModel>> GetAllPostAsync()
        {
            var posts = await _context.Posts!.ToListAsync();
            return _mapper.Map<IEnumerable<PostModel>>(posts);
        }

        public async Task<IEnumerable<PostModel>> GetPostByCollectionIdAsync(int userId, int? collectionId)
        {
            if (collectionId == null)
            {
                var collectionDefault = _context.Collections
                    .FirstOrDefault(c => c.UserId == userId && c.Name.Equals("Bảng mặc định"));
                if (collectionDefault == null)
                {
                    return null!;
                }
                collectionId = collectionDefault!.Id;
            }
            var posts = await _context.Ownerships!
                .Where(o => o.CollectionId == collectionId)
                .Join(_context.Posts, o => o.PostId, p => p.Id, (o, p) => p)
                .ToListAsync();
            return _mapper.Map<IEnumerable<PostModel>>(posts);
        }

        public async Task<PostModel> GetPostByIdAsync(int id)
        {
            var post = await _context.Posts!.FindAsync(id);
            return _mapper.Map<PostModel>(post);
        }

        public async Task<IEnumerable<PostModel>> GetPostByUserIdAsync(int userId)
        {
            var posts = await _context.Posts
                .Where(p => p.UserId == userId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<PostModel>>(posts);
        }

        public async Task<PostModel> UpdatePostAsync(int id, PostUpdateParams entryParams, int userId, IFormFile? file)
        {
            var updatePost = await _context.Posts!.FindAsync(id);
            if (updatePost != null)
            {
                if (file != null)
                {
                    var addResult = await _photoService.AddPhotoAsync(file);
                    if (addResult.Error != null) return null!;

                    var deleteResult = await _photoService.DeletePhotoAsync(updatePost.ThumbnailId);
                    if (deleteResult.Error != null || deleteResult.Result == "not found") return null!;

                    updatePost.ThumbnailId = addResult.PublicId;
                    updatePost.ThumbnailUrl = addResult.SecureUrl.AbsoluteUri;
                }


                if (entryParams.CollectionId != null)
                {
                    var IsExist = await _context.Ownerships
                        .Where(o => o.PostId == id)
                        .AnyAsync(o => o.CollectionId == entryParams.CollectionId);
                    if (!IsExist)
                    {
                        await OwnershipAsync(userId, id, entryParams.CollectionId);
                    }
                }

                _tools.Duplicate(entryParams, ref updatePost);
                _context.Posts!.Update(updatePost);
                await _context.SaveChangesAsync();

                return _mapper.Map<PostModel>(updatePost);
            }
            return null!;
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
                    post!.Like++;
                    _context.Posts.Update(post);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> UpdatePostCountForCollection(int collectionId)
        {
            var collection = await _context.Collections.FindAsync(collectionId);
            if (collection != null)
            {
                collection.PostCount = await _context.Ownerships!
                    .Where(o => o.CollectionId == collectionId)
                    .CountAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> checkLikeAsync(int postId, int userId)
        {
            return await _context.LikePosts
                 .AnyAsync(l => l.UserId == userId && l.PostId == postId);
        }
    }
}
