namespace PixelPalette.Helpers
{
    public class PostInitParams
    {
        public int UserId { get; set; }
        public int? CollectionId { get; set; }
        public string? Link { get; set; }
        public string? Caption { get; set; }
        public string? Detail { get; set; }
        public string? Theme { get; set; }
        public string ThumbnailUrl { get; set; } = null!;
        public string ThumbnailId { get; set; } = null!;
    }
}
