using AegeanLogs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AegeanLogs.Infrastructure.Persistence.Configurations;

public class ClientCompanyConfiguration : IEntityTypeConfiguration<ClientCompany>
{
    public void Configure(EntityTypeBuilder<ClientCompany> builder)
    {
        builder.ToTable("ClientCompanies");

        builder.HasKey(clientCompany => clientCompany.Id);

        builder.Property(clientCompany => clientCompany.Name).IsRequired().HasMaxLength(150);
        builder.Property(clientCompany => clientCompany.ContactEmail).IsRequired().HasMaxLength(250);
        builder.Property(clientCompany => clientCompany.PhoneNumber).HasMaxLength(20);
        
        builder.HasIndex(clientCompany => clientCompany.Name).IsUnique();
        builder.HasIndex(clientCompany => clientCompany.IsActive);
    }
}
