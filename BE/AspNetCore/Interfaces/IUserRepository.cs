using PixelPalette.Entities;
using PixelPalette.Models;

namespace PixelPalette.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(int id, UserModel model);
        Task<bool> UpdateProfileAsync(int id, ProfileModel model);
        Task<bool> UpdateAccountAsync(int id, AccountModel model);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> EditAvatar(int publicId, IFormFile file);
    }
}
