using Microsoft.AspNetCore.Identity;

namespace PixelPalette.Entities
{
    public class Role : IdentityRole<int>
    {
        public Role() {
            UserRoles = new HashSet<IdentityUserRole<int>>();
        }
        public Role(string roleName) : base(roleName)
        {
            UserRoles = new HashSet<IdentityUserRole<int>>();
        }
        public ICollection<IdentityUserRole<int>> UserRoles { get; set; }
    }
}
