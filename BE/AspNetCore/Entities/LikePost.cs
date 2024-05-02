using System;
using System.Collections.Generic;

namespace PixelPalette.Entities
{
    public partial class LikePost
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
