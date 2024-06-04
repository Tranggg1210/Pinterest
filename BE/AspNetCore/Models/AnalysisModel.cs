namespace PixelPalette.Models
{
    public class AnalysisModel
    {
        public int Id { get; set; }
        public int PostCount { get; set; }
        public int UserCount { get; set; }
        public int NotificationCount { get; set; }
        public int AccessCountInDay { get; set; }
        public DateTime Date { get; set; }
    }
}
