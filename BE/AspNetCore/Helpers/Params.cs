namespace PixelPalette.Helpers
{
    public class ChangePasswordParams
    {
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ComfirmPassword { get; set; } = null!;
    }

    public class PostCreateParams
    {
        public int? CollectionId { get; set; }
        public string? Link { get; set; }
        public string Caption { get; set; } = null!;
        public string? Detail { get; set; }
        public string? Theme { get; set; }
    }

    public class PostUpdateParams
    {
        public int? CollectionId { get; set; }
        public string? Link { get; set; }
        public string? Caption { get; set; }
        public string? Detail { get; set; }
        public string? Theme { get; set; }
    }

    public class ProfileParams
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Introduction { get; set; }
        public DateTime? Birthday { get; set; }
        public bool? Gender { get; set; }
        public string? Country { get; set; }
    }

    public class UserPrams
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AvatarId { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Introduction { get; set; }
        public DateTime? Birthday { get; set; }
        public bool? Gender { get; set; }
        public string? Country { get; set; }
    }

    public class CollectCreateParams
    {
        public string Name { get; set; } = null!;
    }

    public class CollectUpdateParams
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class NotificationParam
    {
        public string Data { get; set; } = null!;
    }

    public class  Thumbnail
    {
        public string PublicId { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
}
