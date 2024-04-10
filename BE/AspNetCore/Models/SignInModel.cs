using System.ComponentModel.DataAnnotations;

namespace PixelPalette.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "UserName is required"), EmailAddress]
        public string UserName { get; set; } = null!;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;
    }
}
