namespace PixelPalette.Helpers
{
    public class ChangePasswordParams
    {
        public string UserName { get; set; } = null!;
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ComfirmPassword { get; set; } = null!;

    }
}
