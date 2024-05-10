using PixelPalette.Models;

namespace PixelPalette.Interfaces
{
    public interface IAdminRepository
    {
        Task<IEnumerable<UserRoleModel>> GetUsersWithRole();
        Task<IList<string>> EditRoles(int id, List<string> roles);

    }
}
