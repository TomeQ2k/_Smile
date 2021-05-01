using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Infrastructure.Persistence.Database.Configs
{
    public class GroupMemberConfig : IEntityTypeConfiguration<GroupMember>
    {
        public void Configure(EntityTypeBuilder<GroupMember> builder)
        {
            builder.HasKey(m => new { m.UserId, m.GroupId });

            builder.HasOne(m => m.User)
                    .WithMany(u => u.GroupMembers)
                    .HasForeignKey(m => m.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Group)
                    .WithMany(g => g.GroupMembers)
                    .HasForeignKey(m => m.GroupId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}