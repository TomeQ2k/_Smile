using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smile.Core.Domain.Entities.Group;

namespace Smile.Infrastructure.Persistence.Database.Configs
{
    public class GroupConfig : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasIndex(g => g.Name).IsUnique();

            builder.HasMany(g => g.Posts)
                    .WithOne(p => p.Group)
                    .HasForeignKey(p => p.GroupId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(g => g.Admin)
                    .WithMany(u => u.Groups)
                    .HasForeignKey(g => g.AdminId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(g => g.GroupInvites)
                    .WithOne(i => i.Group)
                    .HasForeignKey(i => i.GroupId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}