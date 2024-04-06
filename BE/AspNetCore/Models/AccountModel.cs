namespace PixelPalette.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public string? Country { get; set; }

    }
}
