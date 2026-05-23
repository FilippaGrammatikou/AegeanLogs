using AegeanLogs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AegeanLogs.Application.Common.Persistence;
public interface IAegeanLogsDbContext
{
    DbSet<ApplicationUser> ApplicationUsers { get;}
    DbSet<ClientCompany> ClientCompanies { get;}
    DbSet<Vessel> Vessels { get;}
    DbSet<Port> Ports { get;}
    DbSet<PortCall> PortCalls { get; }
    DbSet<ServiceType> ServiceTypes { get; }
    DbSet<ServiceRequirementRule> ServiceRequirementRules { get; }
    DbSet<Supplier>Suppliers { get;}
    DbSet<ServiceJob> ServiceJobs { get;}
    DbSet<PortCallDocument>PortCallDocuments { get;}
    DbSet<AuditLogEntry>AuditLogEntries { get; }
}
