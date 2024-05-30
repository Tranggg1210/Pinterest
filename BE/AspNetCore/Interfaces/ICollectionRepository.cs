using CloudinaryDotNet.Actions;
using PixelPalette.Helpers;
using PixelPalette.Models;

namespace PixelPalette.Interfaces
{
    public interface ICollectionRepository
    {
        Task<CollectionModel> AddCollectionAsync(int userId, CollectCreateParams entryParams);
        Task<IEnumerable<CollectionModel>> GetAllCollectionAsync();
        Task<IEnumerable<CollectionModel>> GetCollectionByUserIdAsync(int userId);
        Task<IEnumerable<CollectionModel>> GetCollectionByPostIdAsync(int postId);
        Task<int> CountPostByCollectionId(int id);
        Task<CollectionModel> GetCollectionByIdAsync(int id);
        Task<bool> DeleteCollectionAsync(int id);
        Task<CollectionModel> UpdateCollectionAsync(int id, CollectUpdateParams entryParams);
        Task<string> EditBackgroundAsync(int id, IFormFile file);
        Task<bool> CheckOwnCollectionAsync(int postId, int userId);
    }
}
