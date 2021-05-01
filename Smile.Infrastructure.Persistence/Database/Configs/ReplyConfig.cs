using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Infrastructure.Persistence.Database.Configs
{
    public class ReplyConfig : IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> builder)
        {
            builder.HasMany(r => r.ReplyFiles)
                   .WithOne(f => f.Reply)
                   .HasForeignKey(f => f.ReplyId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}