using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smile.Core.Domain.Entities.Auth;

namespace Smile.Infrastructure.Persistence.Database.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.Username).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasMany(u => u.UserRoles)
                        .WithOne(ur => ur.User)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Tokens)
                        .WithOne(t => t.User)
                        .HasForeignKey(t => t.UserId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Posts)
                        .WithOne(p => p.Author)
                        .HasForeignKey(p => p.AuthorId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Comments)
                        .WithOne(c => c.User)
                        .HasForeignKey(c => c.UserId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Likes)
                        .WithOne(l => l.User)
                        .HasForeignKey(l => l.UserId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.GroupInvites)
                        .WithOne(i => i.User)
                        .HasForeignKey(i => i.UserId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Notifications)
                        .WithOne(n => n.User)
                        .HasForeignKey(n => n.UserId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}