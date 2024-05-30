namespace PixelPalette.Models
{
    public class CollectionModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? BackgroundId { get; set; }
        public string? BackgroundUrl { get; set; }
        public string? Description { get; set; }
        public int PostCount { get; set; }
        public bool IsDefault { get; set; } = false;
    }
}
