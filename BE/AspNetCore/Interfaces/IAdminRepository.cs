using PixelPalette.Models;

namespace PixelPalette.Interfaces
{
    public interface IAdminRepository
    {
        Task<IEnumerable<UserRoleModel>> GetAllRolesAsync();
        Task<IList<string>> EditRoleAsync(int id, List<string> roles);
        Task<UserRoleModel> GetRoleByUserIdAsync(int id);


    }
}
