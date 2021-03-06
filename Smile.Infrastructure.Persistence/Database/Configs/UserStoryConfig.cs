using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smile.Core.Domain.Entities.Story;

namespace Smile.Infrastructure.Persistence.Database.Configs
{
    public class UserStoryConfig : IEntityTypeConfiguration<UserStory>
    {
        public void Configure(EntityTypeBuilder<UserStory> builder)
        {
            builder.HasKey(us => new { us.StoryId, us.UserId });

            builder.HasOne(us => us.Story)
                    .WithMany(s => s.UserStories)
                    .HasForeignKey(us => us.StoryId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(us => us.User)
                    .WithMany(u => u.UserStories)
                    .HasForeignKey(us => us.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}