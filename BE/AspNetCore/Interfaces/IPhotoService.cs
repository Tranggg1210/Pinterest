using CloudinaryDotNet.Actions;

namespace PixelPalette.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
        Task<bool> CheckPublicIdExistAsync(string publicId);
        Task<IEnumerable<string>> GetAllPublicIdsAsync();
    }
}
