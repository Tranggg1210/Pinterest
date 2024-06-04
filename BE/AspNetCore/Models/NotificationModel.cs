using PixelPalette.Entities;

namespace PixelPalette.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Data { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
