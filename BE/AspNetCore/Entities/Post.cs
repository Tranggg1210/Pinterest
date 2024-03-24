using System;
using System.Collections.Generic;

namespace PixelPalette.Entities
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int CollectionId { get; set; }
        public string Reference { get; set; } = null!;
        public string? Caption { get; set; }
        public string Detail { get; set; } = null!;
        public string? Theme { get; set; }
        public string? Media { get; set; }
        public long LikeAmount { get; set; }

        public virtual Collection Collection { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
