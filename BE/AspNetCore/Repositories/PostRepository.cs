using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using PixelPalette.Data;
using PixelPalette.Entities;
using PixelPalette.Helpers;
using PixelPalette.Interfaces;
using PixelPalette.Models;
using PixelPalette.Services;

namespace PixelPalette.Repositories
{
    public class PostRepository : TransmitService, IPostRepository
    {
        private readonly PixelPaletteContext _context;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public PostRepository(PixelPaletteContext context, IMapper mapper, IPhotoService photoService)
        {
            _context = context;
            _mapper = mapper;
            _photoService = photoService;
        }
        public async Task<PostModel> AddPostAsync(PostInitParams param)
        {
            var model = new PostModel();
            Transmit(param, ref model);
            var post = _mapper.Map<Post>(model);
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return _mapper.Map<PostModel>(post);
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var deletePost = _context.Posts!.SingleOrDefault(p => p.Id == id);
            if (deletePost != null)
            {
                var deleteResult = await _photoService.DeletePhotoAsync(deletePost.ThumbnailId);
                if (deleteResult.Error != null) return false;

                _context.Posts!.Remove(deletePost);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteThumbnailAsync(string publicId)
        {
            var deleteResult = await _photoService.DeletePhotoAsync(publicId);
            if (deleteResult.Error != null) return false;
            return true;
        }

        public async Task<IEnumerable<PostModel>> GetAllPostAsync()
        {
            var posts = await _context.Posts!.ToListAsync();
            return _mapper.Map<IEnumerable<PostModel>>(posts);
        }

        public async Task<PostModel> GetPostByIdAsync(int id)
        {
            var post = await _context.Posts!.FindAsync(id);
            return _mapper.Map<PostModel>(post);
        }

        public async Task<PostModel> UpdatePostAsync(int id, PostSetParams param)
        {
            var updatePost = await _context.Posts!.FindAsync(id);
            if (updatePost != null)
            {
                var deleteResult = await _photoService.DeletePhotoAsync(updatePost.ThumbnailId);
                if (deleteResult.Error != null) return null!;

                Transmit(param, ref updatePost);
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
    }
}
