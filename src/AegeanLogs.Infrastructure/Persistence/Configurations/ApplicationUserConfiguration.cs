using AegeanLogs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AegeanLogs.Infrastructure.Persistence.Configurations;
public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("ApplicationUsers");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Email).IsRequired().HasMaxLength(256);
        builder.Property(user => user.DisplayName).IsRequired().HasMaxLength(100);
        builder.Property(user => user.PasswordHash).IsRequired().HasMaxLength(500);
        builder.Property(user => user.Role).IsRequired().HasMaxLength(40);
        builder.Property(user => user.CreatedAt).IsRequired();

        builder.HasIndex(user => user.Role);
        builder.HasIndex(user => user.SupplierId);
        builder.HasIndex(user => user.ClientCompanyId);

        builder.HasOne(user => user.Supplier).WithMany(supplier => supplier.Users)
                                                                       .HasForeignKey(user => user.SupplierId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(user => user.ClientCompany).WithMany(clientCompany => clientCompany.Users)
                                                                                  .HasForeignKey(user => user.ClientCompanyId)
                                                                                  .OnDelete(DeleteBehavior.Restrict);

    }
}
