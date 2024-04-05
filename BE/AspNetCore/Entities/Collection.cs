using System;
using System.Collections.Generic;

namespace PixelPalette.Entities
{
    public partial class Collection
    {
        public Collection()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Background { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Post> Posts { get; set; }
    }
}
