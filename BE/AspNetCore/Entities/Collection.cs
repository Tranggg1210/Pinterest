using System;
using System.Collections.Generic;

namespace PixelPalette.Entities
{
    public partial class Collection
    {
        public Collection()
        {
            Ownerships = new HashSet<Ownership>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? BackgroundId { get; set; }
        public string? BackgroundUrl { get; set; }
        public int PostCount { get; set; }
        public bool IsDefault { get; set; } = false;

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Ownership> Ownerships { get; set; }
    }
}
