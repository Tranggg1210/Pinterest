using CloudinaryDotNet.Actions;
using PixelPalette.Helpers;
using PixelPalette.Models;

namespace PixelPalette.Interfaces
{
    public interface IPostRepository
    {
        Task<PostModel> AddPostAsync(PostInitParams param);
        Task<IEnumerable<PostModel>> GetAllPostAsync();
        Task<PostModel> GetPostByIdAsync(int id);
        Task<PostModel> UpdatePostAsync(int id, PostSetParams param);
        Task<bool> DeletePostAsync(int id);
        Task<ImageUploadResult> UploadThumbnailAsync(IFormFile file);
        Task<bool> DeleteThumbnailAsync(string publicId);
    }
}
