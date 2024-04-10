namespace PixelPalette.Models
{
    public class ProfileModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Introduction { get; set; }
    }
}
