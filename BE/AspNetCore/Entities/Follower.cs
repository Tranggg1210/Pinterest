using System;
using System.Collections.Generic;

namespace PixelPalette.Entities
{
    public partial class Follower
    {
        public int Id { get; set; }
        public int FollowingUserId { get; set; }
        public int FollowerUserId { get; set; }
        public bool Status { get; set; }

        public virtual User FollowerUser { get; set; } = null!;
        public virtual User FollowingUser { get; set; } = null!;
    }
}
