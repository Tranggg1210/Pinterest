using PixelPalette.Entities;
using PixelPalette.Models;

namespace PixelPalette.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByIdAsync(int id);
        Task<ProfileModel> UpdateProfileAsync(int id, ProfileModel model);
        Task<AccountModel> UpdateAccountAsync(int id, AccountModel model);
        Task<bool> DeleteUserAsync(int id);
        Task<string> EditAvatar(int publicId, IFormFile file);
    }
}
