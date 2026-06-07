using AegeanLogs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AegeanLogs.Infrastructure.Persistence.Configurations;

public class PortCallConfiguration : IEntityTypeConfiguration<PortCall>
{
    public void Configure(EntityTypeBuilder<PortCall> builder)
    {
        builder.ToTable("PortCalls");

        builder.HasKey(portCall => portCall.Id);

        builder.Property(portCall => portCall.Purpose).IsRequired().HasConversion<string>().HasMaxLength(150);
        builder.Property(portCall => portCall.Status).IsRequired().HasConversion<string>().HasMaxLength(80);
        builder.Property(portCall => portCall.Eta).IsRequired();
        builder.Property(portCall => portCall.Etd).IsRequired();
        builder.Property(portCall => portCall.Notes).HasMaxLength(1000);
        builder.Property(portCall => portCall.CreatedAt).IsRequired();

        builder.HasIndex(portCall => portCall.Status);
        builder.HasIndex(portCall => portCall.Eta);
        builder.HasIndex(portCall => portCall.Etd);
        builder.HasIndex(portCall => new {portCall.PortId,portCall.Status,
            portCall.Eta});

        builder.HasIndex(portCall => new { 
            portCall.VesselId,
            portCall.Eta});

        builder.HasIndex(portCall => portCall.AssignedAgentUserId);

        builder.HasOne(portCall => portCall.Vessel).WithMany(vessel => vessel.PortCalls)
                                                                                .HasForeignKey(portCall => portCall.VesselId)
                                                                                .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(portCall => portCall.Port).WithMany(port => port.PortCalls)
                                                                            .HasForeignKey(portCall => portCall.PortId)
                                                                            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(portCall => portCall.AssignedAgent).WithMany()
                                                                                              .HasForeignKey(portCall => portCall.AssignedAgentUserId)
                                                                                              .OnDelete(DeleteBehavior.Restrict);


    }
}
