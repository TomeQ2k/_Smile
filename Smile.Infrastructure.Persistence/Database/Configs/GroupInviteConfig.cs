using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Infrastructure.Persistence.Database.Configs
{
    public class GroupInviteConfig : IEntityTypeConfiguration<GroupInvite>
    {
        public void Configure(EntityTypeBuilder<GroupInvite> builder)
        {
            builder.HasKey(i => new { i.UserId, i.GroupId });
        }
    }
}