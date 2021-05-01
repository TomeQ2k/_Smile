using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smile.Core.Domain.Entities.Main;

namespace Smile.Infrastructure.Persistence.Database.Configs
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasMany(p => p.Comments)
                    .WithOne(c => c.Post)
                    .HasForeignKey(c => c.PostId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Likes)
                    .WithOne(l => l.Post)
                    .HasForeignKey(l => l.PostId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}