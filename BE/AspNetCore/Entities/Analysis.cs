namespace PixelPalette.Entities
{
    public class Analysis
    {
        public int Id { get; set; }
        public int PostCount { get; set; }
        public int UserCount { get; set; }
        public int NotificationCount { get; set; }
        public int AccessCountInDay { get; set; }
        public DateTime CreateAt { get; set; }

    }
}
