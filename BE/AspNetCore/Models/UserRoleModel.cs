namespace PixelPalette.Models
{
    public class UserRoleModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public List<string> Roles { get; set; } = null!;
    }
}
