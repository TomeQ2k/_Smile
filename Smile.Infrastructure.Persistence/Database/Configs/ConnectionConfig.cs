using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smile.Core.Domain.Entities.Connection;

namespace Smile.Infrastructure.Persistence.Database.Configs
{
    public class ConnectionConfig : IEntityTypeConfiguration<Connection>
    {
        public void Configure(EntityTypeBuilder<Connection> builder)
        {
            builder.HasKey(c => new { c.UserId, c.ConnectionId });

            builder.HasOne(c => c.User)
                    .WithMany(u => u.Connections)
                    .HasForeignKey(c => c.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}