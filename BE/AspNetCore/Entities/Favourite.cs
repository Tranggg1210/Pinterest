using System;
using System.Collections.Generic;

namespace PixelPalette.Entities
{
    public partial class Favourite
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
