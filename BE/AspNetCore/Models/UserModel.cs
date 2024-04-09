namespace PixelPalette.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string? Introduction { get; set; }
        public string Email { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public string? Country { get; set; }
    }
}
