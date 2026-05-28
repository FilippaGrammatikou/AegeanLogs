using AegeanLogs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AegeanLogs.Infrastructure.Persistence.Configurations;
public class VesselConfiguration : IEntityTypeConfiguration<Vessel>
{
    public void Configure(EntityTypeBuilder<Vessel> builder)
    {
        builder.ToTable("Vessels");

        builder.HasKey(vessel => vessel.Id);

        builder.Property(vessel => vessel.Name).IsRequired().HasMaxLength(150);
        builder.Property(vessel => vessel.ImoNumber).IsRequired().HasMaxLength(20);
        builder.Property(vessel => vessel.VesselType).IsRequired().HasMaxLength(100);
        builder.Property(vessel => vessel.Flag).IsRequired().HasMaxLength(100);

        builder.HasIndex(vessel => vessel.ImoNumber).IsUnique();
        builder.HasIndex(vessel => vessel.ClientCompanyId);
        builder.HasIndex(vessel => vessel.IsActive);

        builder.HasOne(vessel => vessel.ClientCompany).WithMany(clientCompany => clientCompany.Vessels)
                                                                                       .HasForeignKey(vessel => vessel.ClientCompanyId)
                                                                                       .OnDelete(DeleteBehavior.Restrict);

    }
}
