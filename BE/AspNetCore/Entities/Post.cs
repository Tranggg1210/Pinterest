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
        public int? CollectionId { get; set; }
        public string? Link { get; set; }
        public string? Caption { get; set; } 
        public string? Detail { get; set; }
        public string? Theme { get; set; }
        public string ThumbnailUrl { get; set; } = null!;
        public string ThumbnailId { get; set; } = null!;
        public long LikeAmount { get; set; }

        public virtual Collection? Collection { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
