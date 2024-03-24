using PixelPalette.Entities;
using PixelPalette.Models;

namespace PixelPalette.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<UserModel>> GetAllUsersAsync();
        public Task<UserModel> GetUserByIdAsync(int id);
        public Task<int> AddUserAsync(UserModel model);
        public Task UpdateUserAsync(int id, UserModel model);
        public Task DeleteUserAsync(int id);
    }
}
