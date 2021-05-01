using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smile.Core.Domain.Entities.Community;

namespace Smile.Infrastructure.Persistence.Database.Configs
{
    public class FriendConfig : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.HasKey(f => new { f.SenderId, f.RecipientId });

            builder.HasOne(f => f.Sender)
                    .WithMany(u => u.FriendsSent)
                    .HasForeignKey(f => f.SenderId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.Recipient)
                    .WithMany(r => r.FriendsReceived)
                    .HasForeignKey(f => f.RecipientId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}