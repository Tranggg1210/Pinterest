using System;
using System.Collections.Generic;

namespace PixelPalette.Entities
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            LikePosts = new HashSet<LikePost>();
            Ownerships = new HashSet<Ownership>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Caption { get; set; } = null!;
        public string? Detail { get; set; }
        public string? Theme { get; set; }
        public string? Link { get; set; }
        public string ThumbnailId { get; set; } = null!;
        public string ThumbnailUrl { get; set; } = null!;
        public long Like { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<LikePost> LikePosts { get; set; }
        public virtual ICollection<Ownership> Ownerships { get; set; }
    }
}
