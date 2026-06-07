using AegeanLogs.Application.Common.Persistence;
using AegeanLogs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AegeanLogs.Application.Persistence;
public class AegeanLogsDbContext : DbContext, IAegeanLogsDbContext
{
    public AegeanLogsDbContext(DbContextOptions<AegeanLogsDbContext> options) : base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    public DbSet<ClientCompany> ClientCompanies => Set<ClientCompany>();
    public DbSet<Vessel> Vessels => Set<Vessel>();
    public DbSet<Port> Ports => Set<Port>();
    public DbSet<PortCall> PortCalls => Set<PortCall>();
    public DbSet<ServiceType> ServiceTypes => Set<ServiceType>();
    public DbSet<ServiceRequirementRule> ServiceRequirementRules => Set<ServiceRequirementRule>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<ServiceJob> ServiceJobs => Set<ServiceJob>();
    public DbSet<PortCallDocument> PortCallDocuments => Set<PortCallDocument>();
    public DbSet<AuditLogEntry> AuditLogEntries => Set<AuditLogEntry>();

    //EF core will automatically discover whatever new entity config files created
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AegeanLogsDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
