using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        public virtual DbSet<Favourite> Favourites { get; set; } = null!;
        public virtual DbSet<Follower> Followers { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Collection>(entity =>
            {
                entity.ToTable("Collection");

                entity.HasIndex(e => e.UserId, "IX_Collection_UserId");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("Id");

                entity.Property(e => e.Background)
                    .HasMaxLength(255)
                    .HasColumnName("Background");

                entity.Property(e => e.Description).HasColumnName("Description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("Name");

                entity.Property(e => e.Status).HasColumnName("Status");

                entity.Property(e => e.UserId).HasColumnName("UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Collections)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Collection_UserId_foreign");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.HasIndex(e => e.PostId, "IX_Comment_PostId");

                entity.HasIndex(e => e.UserId, "IX_Comment_UserId");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.CommentReplyToId)
                    .HasMaxLength(255)
                    .HasColumnName("CommentReplyId");

                entity.Property(e => e.Content).HasColumnName("Content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedAt");

                entity.Property(e => e.LikeAmount)
                    .HasColumnName("LikeAmount")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.PostId).HasColumnName("PostId");

                entity.Property(e => e.UserId).HasColumnName("UserId");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Comment_PostId_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Comment_UserId_foreign");
            });

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.ToTable("Conversation");

                entity.HasIndex(e => e.CreatedByUserId, "IX_Conversation_CreatedByUserId");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedAt");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserId");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.Conversations)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Conversation_CreatedByUserId_foreign");
            });

            modelBuilder.Entity<Favourite>(entity =>
            {
                entity.ToTable("Favourite");

                entity.HasIndex(e => e.UserId, "IX_Favourite_UserId");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .HasColumnName("Image");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("Name");

                entity.Property(e => e.UserId).HasColumnName("UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Favourites)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Favourite_UserId_foreign");
            });

            modelBuilder.Entity<Follower>(entity =>
            {
                entity.ToTable("Follower");

                entity.HasIndex(e => e.FollowerUserId, "IX_Follower_FollowerUserId");

                entity.HasIndex(e => e.FollowingUserId, "IX_Follower_FollowingUserId");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.FollowerUserId).HasColumnName("FollowerUserId");

                entity.Property(e => e.FollowingUserId).HasColumnName("FollowingUserId");

                entity.Property(e => e.Status).HasColumnName("Status");

                entity.HasOne(d => d.FollowerUser)
                    .WithMany()
                    .HasForeignKey(d => d.FollowerUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Follower_FollowerUserId_foreign");

                entity.HasOne(d => d.FollowingUser)
                    .WithMany()
                    .HasForeignKey(d => d.FollowingUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Follower_FollowingUserId_foreign");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.HasIndex(e => e.ConversationId, "IX_Message_ConversationId");

                entity.HasIndex(e => e.CreatedByUserId, "IX_Message_CreatedByUserId");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.Content).HasColumnName("Content");

                entity.Property(e => e.ConversationId).HasColumnName("ConversationId");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedAt")
                    .HasDefaultValueSql("('2024-03-17 14:29:45')");

                entity.Property(e => e.CreatedByUserId).HasColumnName("CreatedByUserId");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .HasColumnName("Status");

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ConversationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Message_ConversationId_foreign");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Message_CreatedByUserId_foreign");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.HasIndex(e => e.CollectionId, "IX_Post_CollectionId");

                entity.HasIndex(e => e.UserId, "IX_Post_UserId");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.Caption)
                    .HasMaxLength(255)
                    .HasColumnName("Caption");

                entity.Property(e => e.CollectionId).HasColumnName("CollectionId");

                entity.Property(e => e.Detail).HasColumnName("Detail");

                entity.Property(e => e.LikeAmount)
                    .HasColumnName("LikeAmount")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Media)
                    .HasMaxLength(255)
                    .HasColumnName("Media");

                entity.Property(e => e.Reference)
                    .HasMaxLength(255)
                    .HasColumnName("Reference");

                entity.Property(e => e.Theme)
                    .HasMaxLength(255)
                    .HasColumnName("Theme");

                entity.Property(e => e.UserId).HasColumnName("UserId");

                entity.HasOne(d => d.Collection)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CollectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Post_CollectionId_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Post_UserId_foreign");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "IX_User_Email");

                entity.HasIndex(e => e.UserName, "IX_User_UserName");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.UserName).IsRequired();

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasColumnName("Birthday");

                entity.Property(e => e.Country)
                    .HasMaxLength(255)
                    .HasColumnName("Country");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .HasColumnName("FirstName");

                entity.Property(e => e.Gender).HasColumnName("Gender");

                entity.Property(e => e.Token).HasColumnName("Token");

                entity.Property(e => e.Introduction).HasColumnName("Introduction");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .HasColumnName("LastName");

                entity.Ignore("AccessFailedCount");

                entity.Ignore("ConcurrencyStamp");

                entity.Ignore("EmailConfirmed");

                entity.Ignore("LockoutEnabled");

                entity.Ignore("LockoutEnd");

                entity.Ignore("PhoneNumber");

                entity.Ignore("PhoneNumberConfirmed");

                entity.Ignore("SecurityStamp");

                entity.Ignore("TwoFactorEnabled");

            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
