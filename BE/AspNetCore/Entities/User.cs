using Microsoft.AspNetCore.Identity;

namespace PixelPalette.Entities
{
    public partial class User : IdentityUser<int>
    {
        public User()
        {
            Collections = new HashSet<Collection>();
            Comments = new HashSet<Comment>();
            Conversations = new HashSet<Conversation>();
            Favourites = new HashSet<Favourite>();
            Messages = new HashSet<Message>();
            Posts = new HashSet<Post>();
        }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string? Introduction { get; set; }
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public string? Country { get; set; } = null!;

        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Conversation> Conversations { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
