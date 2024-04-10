using System.ComponentModel.DataAnnotations;

namespace PixelPalette.Models
{
    public class SignUpModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required(ErrorMessage ="UserName is required"), EmailAddress]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;
    }
}
