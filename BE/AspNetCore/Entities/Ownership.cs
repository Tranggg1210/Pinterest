using System;
using System.Collections.Generic;

namespace PixelPalette.Entities
{
    public partial class Ownership
    {
        public int Id { get; set; }
        public int CollectionId { get; set; }
        public int PostId { get; set; }

        public virtual Collection Collection { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}
