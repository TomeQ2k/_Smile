using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smile.Core.Domain.Entities.Main;

namespace Smile.Infrastructure.Persistence.Database.Configs
{
    public class LikeConfig : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(l => new { l.UserId, l.PostId });
        }
    }
}