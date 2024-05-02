using CloudinaryDotNet.Actions;
using PixelPalette.Helpers;
using PixelPalette.Models;
using System.Runtime.CompilerServices;

namespace PixelPalette.Interfaces
{
    public interface IPostRepository
    {
        Task<PostModel> AddPostAsync(int userId, PostCreateParams entryParams);
        Task<IEnumerable<PostModel>> GetAllPostAsync();
        Task<PostModel> GetPostByIdAsync(int id);
        Task<PostModel> UpdatePostAsync(int id, PostUpdateParams entryParams, Thumbnail thumbnail);
        Task<bool> DeletePostAsync(int id);
        Task<ImageUploadResult> UploadThumbnailAsync(IFormFile file);
        Task<bool> DeleteThumbnailAsync(string publicId);
        Task<PostModel> GetPostByCollectionIdAsync(int collectionId);
        Task<PostModel> GetPostByUserIdAsync(int userId);
        Task<bool> OwnershipAsync(int postId, int collectionId);
        Task<bool> LikePostAsync(int postId, int userId);
    }
}
