using AegeanLogs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AegeanLogs.Infrastructure.Persistence.Configurations;

public class AuditLogEntryConfiguration : IEntityTypeConfiguration<AuditLogEntry>
{
    public void Configure(EntityTypeBuilder<AuditLogEntry> builder)
    {
        builder.ToTable("AuditLogEntries");

        builder.HasKey(auditLogEntry => auditLogEntry.Id);

        builder.Property(auditLogEntry => auditLogEntry.ActionType).IsRequired().HasConversion<string>().HasMaxLength(100);
        builder.Property(auditLogEntry => auditLogEntry.EntityName).IsRequired().HasMaxLength(100);
        builder.Property(auditLogEntry => auditLogEntry.OldValue).HasMaxLength(2000);
        builder.Property(auditLogEntry => auditLogEntry.NewValue).HasMaxLength(2000);
        builder.Property(auditLogEntry => auditLogEntry.Summary).IsRequired().HasMaxLength(1000);
        builder.Property(auditLogEntry => auditLogEntry.CreatedAt).IsRequired();

        builder.HasIndex(auditLogEntry => new { auditLogEntry.PortCallId, auditLogEntry.CreatedAt });
        builder.HasIndex(auditLogEntry => new { auditLogEntry.UserId, auditLogEntry.CreatedAt });

        builder.HasIndex(auditLogEntry => auditLogEntry.ActionType);
        builder.HasIndex(auditLogEntry=> new {auditLogEntry.EntityName, auditLogEntry.EntityId});

        builder.HasOne(auditLogEntry => auditLogEntry.PortCall).WithMany(portCall => portCall.AuditLogEntries)
                                                                                                       .HasForeignKey(auditLogEntry => auditLogEntry.PortCallId)
                                                                                                       .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(auditLogEntry => auditLogEntry.User).WithMany()
                                                                                                  .HasForeignKey(auditLogEntry => auditLogEntry.UserId)
                                                                                                  .OnDelete(DeleteBehavior.Restrict);
    }
}
