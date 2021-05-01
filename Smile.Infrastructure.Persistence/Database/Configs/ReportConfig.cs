using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smile.Core.Domain.Entities.Support;

namespace Smile.Infrastructure.Persistence.Database.Configs
{
    public class ReportConfig : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasOne(r => r.Reporter)
                    .WithMany(u => u.Reports)
                    .HasForeignKey(r => r.ReporterId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(r => r.Replies)
                      .WithOne(r => r.Report)
                      .HasForeignKey(r => r.ReportId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.ReportFiles)
                      .WithOne(f => f.Report)
                      .HasForeignKey(f => f.ReportId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
        }
    }
}