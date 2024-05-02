using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace PixelPalette.Entities
{
    public partial class User : IdentityUser<int>
    {
        public User()
        {
            Collections = new HashSet<Collection>();
            Comments = new HashSet<Comment>();
            ConversationConnectors = new HashSet<Conversation>();
            ConversationCreators = new HashSet<Conversation>();
            FollowerFollowerUsers = new HashSet<Follower>();
            FollowerFollowingUsers = new HashSet<Follower>();
            LikePosts = new HashSet<LikePost>();
            MessageRecipients = new HashSet<Message>();
            MessageSenders = new HashSet<Message>();
            Notifications = new HashSet<Notification>();
            Posts = new HashSet<Post>();
        }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Introduction { get; set; }
        public string? AvatarId { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime? Birthday { get; set; } = DateTime.UtcNow;
        public bool? Gender { get; set; } = true;
        public string? Country { get; set; }
        public string? Token { get; set; }
        public long Follower { get; set; }
        public long Following { get; set; }

        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Conversation> ConversationConnectors { get; set; }
        public virtual ICollection<Conversation> ConversationCreators { get; set; }
        public virtual ICollection<Follower> FollowerFollowerUsers { get; set; }
        public virtual ICollection<Follower> FollowerFollowingUsers { get; set; }
        public virtual ICollection<LikePost> LikePosts { get; set; }
        public virtual ICollection<Message> MessageRecipients { get; set; }
        public virtual ICollection<Message> MessageSenders { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
