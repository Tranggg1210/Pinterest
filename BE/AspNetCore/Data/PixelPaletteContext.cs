using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PixelPalette.Entities;

namespace PixelPalette.Data
{
    public partial class PixelPaletteContext : DbContext
    {
        public PixelPaletteContext()
        {
        }

        public PixelPaletteContext(DbContextOptions<PixelPaletteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Collection> Collections { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Conversation> Conversations { get; set; } = null!;
        public virtual DbSet<Follower> Followers { get; set; } = null!;
        public virtual DbSet<LikePost> LikePosts { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Ownership> Ownerships { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=MSI;Initial Catalog=PixelPalette;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Collection>(entity =>
            {
                entity.ToTable("Collection");

                entity.HasIndex(e => e.UserId, "IX_Collection_UserId");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Collections)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Collection_UserId_foreign");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.HasIndex(e => e.PostId, "IX_Comment_PostId");

                entity.HasIndex(e => e.UserId, "IX_Comment_UserId");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('0001-01-01T00:00:00.000')");

                entity.Property(e => e.Like).HasDefaultValueSql("('0')");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Comment_PostId_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Comment_UserId_foreign");
            });

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.ToTable("Conversation");

                entity.HasIndex(e => e.ConnectorId, "IX_Conversation_ConnectorId");

                entity.HasIndex(e => e.CreatorId, "IX_Conversation_CreatorId");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Connector)
                    .WithMany(p => p.ConversationConnectors)
                    .HasForeignKey(d => d.ConnectorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Conversation_User");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.ConversationCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Conversation_User1");
            });

            modelBuilder.Entity<Follower>(entity =>
            {
                entity.ToTable("Follower");

                entity.HasIndex(e => e.FollowerUserId, "IX_Follower_FollowerUserId");

                entity.HasIndex(e => e.FollowingUserId, "IX_Follower_FollowingUserId");

                entity.HasOne(d => d.FollowerUser)
                    .WithMany(p => p.FollowerFollowerUsers)
                    .HasForeignKey(d => d.FollowerUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Follower_FollowerUserId_foreign");

                entity.HasOne(d => d.FollowingUser)
                    .WithMany(p => p.FollowerFollowingUsers)
                    .HasForeignKey(d => d.FollowingUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Follower_FollowingUserId_foreign");
            });

            modelBuilder.Entity<LikePost>(entity =>
            {
                entity.ToTable("LikePost");

                entity.HasIndex(e => e.PostId, "IX_LikePost_PostId");

                entity.HasIndex(e => e.UserId, "IX_LikePost_UserId");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.LikePosts)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_LikePost_Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LikePosts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_LikePost_User");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.HasIndex(e => e.ConversationId, "IX_Message_ConversationId");

                entity.HasIndex(e => e.RecipientId, "IX_Message_RecipientId");

                entity.HasIndex(e => e.SenderId, "IX_Message_SenderId");

                entity.Property(e => e.DateRead).HasColumnType("datetime");

                entity.Property(e => e.DateSent)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('2024-03-17 14:29:45')");

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ConversationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Message_ConversationId_foreign");

                entity.HasOne(d => d.Recipient)
                    .WithMany(p => p.MessageRecipients)
                    .HasForeignKey(d => d.RecipientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Message_RecipientId_foreign");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageSenders)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Message_SenderId_foreign");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.HasIndex(e => e.UserId, "IX_Notification_UserId");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Notification_User");
            });

            modelBuilder.Entity<Ownership>(entity =>
            {
                entity.ToTable("Ownership");

                entity.HasIndex(e => e.CollectionId, "IX_Ownership_CollectionId");

                entity.HasIndex(e => e.PostId, "IX_Ownership_PostId");

                entity.HasOne(d => d.Collection)
                    .WithMany(p => p.Ownerships)
                    .HasForeignKey(d => d.CollectionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Ownership_Collection");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Ownerships)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Ownership_Post");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.HasIndex(e => e.UserId, "IX_Post_UserId");

                entity.Property(e => e.Caption).HasMaxLength(255);

                entity.Property(e => e.Like).HasDefaultValueSql("('0')");

                entity.Property(e => e.Link).HasMaxLength(255);

                entity.Property(e => e.Theme).HasMaxLength(255);

                entity.Property(e => e.ThumbnailId).HasDefaultValueSql("(N'')");

                entity.Property(e => e.ThumbnailUrl).HasDefaultValueSql("(N'')");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Post_UserId_foreign");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.HasIndex(e => e.NormalizedName, "IX_Role_NormalizedName").IsUnique();

            });

            modelBuilder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("UserRole");

                entity.HasKey(ur => new { ur.UserId, ur.RoleId });
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "IX_User_Email");

                entity.HasIndex(e => e.UserName, "IX_User_UserName").IsUnique();

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Country).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.Follower).HasDefaultValueSql("('0')");

                entity.Property(e => e.Following).HasDefaultValueSql("('0')");

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Ignore("AccessFailedCount");

                entity.Ignore("ConcurrencyStamp");

                entity.Ignore("EmailConfirmed");

                entity.Ignore("LockoutEnabled");

                entity.Ignore("LockoutEnd");

                entity.Ignore("PhoneNumber");

                entity.Ignore("PhoneNumberConfirmed");

                entity.Ignore("TwoFactorEnabled");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
