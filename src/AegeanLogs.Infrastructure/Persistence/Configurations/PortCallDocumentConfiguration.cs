using AegeanLogs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AegeanLogs.Infrastructure.Persistence.Configurations;
public class PortCallDocumentConfiguration : IEntityTypeConfiguration<PortCallDocument>
{
    public void Configure(EntityTypeBuilder<PortCallDocument> builder)
    {
        builder.ToTable("PortCallDocuments");

        builder.HasKey(document => document.Id);

        builder.Property(document => document.DocumentType).IsRequired().HasMaxLength(100);
        builder.Property(document => document.FileName).HasMaxLength(300);
        builder.Property(document=> document.Status).IsRequired().HasConversion<string>().HasMaxLength(100);
        builder.Property(document => document.RejectionReason).HasMaxLength(1000);

        builder.HasIndex(document => document.PortCallId);
        builder.HasIndex(document => document.Status);
        builder.HasIndex(document => new {document.PortCallId, document.Status});
        builder.HasIndex(document => document.UploadedByUserId);
        builder.HasIndex(document => document.CheckedByUserId);

        builder.HasOne(document=> document.PortCall).WithMany(portCall=> portCall.Documents)
                                                                                       .HasForeignKey(document=> document.PortCallId)
                                                                                       .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(document => document.UploadedByUser).WithMany()
                                                                                       .HasForeignKey(document => document.UploadedByUserId)
                                                                                       .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(document => document.CheckedByUser).WithMany()
                                                                                       .HasForeignKey(document => document.CheckedByUserId)
                                                                                       .OnDelete(DeleteBehavior.Restrict);
    }
}
