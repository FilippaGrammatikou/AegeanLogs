using AegeanLogs.Domain.Entities;
using AegeanLogs.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AegeanLogs.Infrastructure.Persistence.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AegeanLogsDbContext dbContext,CancellationToken cancellationToken = default)
    {
        await SeedPortsAsync(dbContext, cancellationToken);
        await SeedServiceTypesAsync(dbContext, cancellationToken);
        await SeedClientCompaniesAsync(dbContext, cancellationToken);
        await SeedVesselsAsync(dbContext, cancellationToken);
        await SeedSuppliersAsync(dbContext, cancellationToken);
        await SeedUsersAsync(dbContext, cancellationToken);
        await SeedServiceRequirementRulesAsync(dbContext, cancellationToken);
    }

    private static async Task SeedPortsAsync(AegeanLogsDbContext dbContext,CancellationToken cancellationToken)
    {
        var existingUnLocodes = await dbContext.Ports.AsNoTracking().Select(port => port.UnLocode).ToListAsync(cancellationToken);
        var existingUnLocodeSet = existingUnLocodes.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var missingPorts = SeedData.Ports().Where(port => !existingUnLocodeSet.Contains(port.UnLocode)).ToList();

        if (missingPorts.Count == 0)
        {
            return;
        }

        dbContext.Ports.AddRange(missingPorts);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task SeedServiceTypesAsync(AegeanLogsDbContext dbContext,CancellationToken cancellationToken)
    {
        var existingCodes = await dbContext.ServiceTypes.AsNoTracking().Select(serviceType => serviceType.Code).ToListAsync(cancellationToken);
        var existingCodeSet = existingCodes.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var missingServiceTypes = SeedData.ServiceTypes().Where(serviceType => !existingCodeSet.Contains(serviceType.Code)).ToList();

        if (missingServiceTypes.Count == 0)
        {
            return;
        }

        dbContext.ServiceTypes.AddRange(missingServiceTypes);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task SeedClientCompaniesAsync(AegeanLogsDbContext dbContext,CancellationToken cancellationToken)
    {
        var existingCodes = await dbContext.ClientCompanies.AsNoTracking().Select(company => company.Code).ToListAsync(cancellationToken);
        var existingCodeSet = existingCodes.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var missingCompanies = SeedData.ClientCompanies().Where(company => !existingCodeSet.Contains(company.Code)).ToList();

        if (missingCompanies.Count == 0)
        {
            return;
        }

        dbContext.ClientCompanies.AddRange(missingCompanies);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task SeedVesselsAsync(AegeanLogsDbContext dbContext,CancellationToken cancellationToken)
    {
        var companyRecords = await dbContext.ClientCompanies.AsNoTracking().Select(company => new{company.Id,company.Code}).ToListAsync(cancellationToken);
        var companyIdsByCode = companyRecords.ToDictionary(company => company.Code, company => company.Id,StringComparer.OrdinalIgnoreCase);
        var aegeanBlueCompanyId = GetRequiredId(companyIdsByCode, "AEGEAN_BLUE", "client company");
        var hellenicBulkCompanyId = GetRequiredId(companyIdsByCode, "HELLENIC_BULK", "client company");

        var intendedVessels = new List<Vessel>
        {
            new()
            {
                ClientCompanyId = aegeanBlueCompanyId,
                Name = "MV Aegean Star",
                ImoNumber = "IMO9300001",
                VesselType = "Container Ship",
                Flag = "Greece",
                IsActive = true
            },

            new()
            {
                ClientCompanyId = aegeanBlueCompanyId,
                Name = "MV Cyclades Trader",
                ImoNumber = "IMO9300002",
                VesselType = "Bulk Carrier",
                Flag = "Malta",
                IsActive = true
            },

            new()
            {
                ClientCompanyId = hellenicBulkCompanyId,
                Name = "MV Thermaikos",
                ImoNumber = "IMO9300003",
                VesselType = "Bulk Carrier",
                Flag = "Greece",
                IsActive = true
            }
        };

        var existingImoNumbers = await dbContext.Vessels.AsNoTracking().Select(vessel => vessel.ImoNumber).ToListAsync(cancellationToken);
        var existingImoNumberSet = existingImoNumbers.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var missingVessels = intendedVessels.Where(vessel => !existingImoNumberSet.Contains(vessel.ImoNumber)).ToList();

        if (missingVessels.Count == 0)
        {
            return;
        }

        dbContext.Vessels.AddRange(missingVessels);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task SeedSuppliersAsync(AegeanLogsDbContext dbContext,CancellationToken cancellationToken)
    {
        var existingCodes = await dbContext.Suppliers.AsNoTracking().Select(supplier => supplier.Code).ToListAsync(cancellationToken);
        var existingCodeSet = existingCodes.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var missingSuppliers = SeedData.Suppliers().Where(supplier => !existingCodeSet.Contains(supplier.Code)).ToList();

        if (missingSuppliers.Count == 0)
        {
            return;
        }

        dbContext.Suppliers.AddRange(missingSuppliers);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task SeedUsersAsync(AegeanLogsDbContext dbContext,CancellationToken cancellationToken)
    {
        var clientCompanyRecords = await dbContext.ClientCompanies.AsNoTracking().Select(company => new{company.Id,company.Code}).ToListAsync(cancellationToken);
        var clientCompanyIdsByCode = clientCompanyRecords.ToDictionary(company => company.Code,company => company.Id,StringComparer.OrdinalIgnoreCase);
        var supplierRecords = await dbContext.Suppliers.AsNoTracking().Select(supplier => new{supplier.Id,supplier.Code }).ToListAsync(cancellationToken);
        var supplierIdsByCode = supplierRecords.ToDictionary(supplier => supplier.Code,supplier => supplier.Id,StringComparer.OrdinalIgnoreCase);
        var clientCompanyId = GetRequiredId(clientCompanyIdsByCode, "AEGEAN_BLUE", "client company");
        var supplierId = GetRequiredId(supplierIdsByCode, "PIRAEUS_MARINE_SUPPLIES", "supplier");

        var intendedUsers = new List<ApplicationUser>
        {
            new()
            {
                Email = "admin@aegeanlogs.local",
                DisplayName = "Demo Admin",
                PasswordHash = "DEV_ONLY_PASSWORD_HASH_NOT_REAL",
                Role = UserRole.Admin,
                IsActive = true
            },

            new()
            {
                Email = "agent@aegeanlogs.local",
                DisplayName = "Demo Port Agent",
                PasswordHash = "DEV_ONLY_PASSWORD_HASH_NOT_REAL",
                Role = UserRole.PortAgent,
                IsActive = true
            },

            new()
            {
                Email = "docs@aegeanlogs.local",
                DisplayName = "Demo Documentation Officer",
                PasswordHash = "DEV_ONLY_PASSWORD_HASH_NOT_REAL",
                Role = UserRole.DocumentationOfficer,
                IsActive = true
            },

            new()
            {
                Email = "manager@aegeanlogs.local",
                DisplayName = "Demo Operations Manager",
                PasswordHash = "DEV_ONLY_PASSWORD_HASH_NOT_REAL",
                Role = UserRole.OperationsManager,
                IsActive = true
            },

            new()
            {
                Email = "supplier@aegeanlogs.local",
                DisplayName = "Demo Supplier User",
                PasswordHash = "DEV_ONLY_PASSWORD_HASH_NOT_REAL",
                Role = UserRole.SupplierUser,
                SupplierId = supplierId,
                IsActive = true
            },

            new()
            {
                Email = "client@aegeanlogs.local",
                DisplayName = "Demo Client User",
                PasswordHash = "DEV_ONLY_PASSWORD_HASH_NOT_REAL",
                Role = UserRole.ClientUser,
                ClientCompanyId = clientCompanyId,
                IsActive = true
            }
        };

        var existingEmails = await dbContext.ApplicationUsers.AsNoTracking().Select(user => user.Email).ToListAsync(cancellationToken);
        var existingEmailSet = existingEmails.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var missingUsers = intendedUsers.Where(user => !existingEmailSet.Contains(user.Email)).ToList();

        if (missingUsers.Count == 0)
        {
            return;
        }

        dbContext.ApplicationUsers.AddRange(missingUsers);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task SeedServiceRequirementRulesAsync(AegeanLogsDbContext dbContext,CancellationToken cancellationToken)
    {
        var serviceTypeRecords = await dbContext.ServiceTypes.AsNoTracking().Select(serviceType => new{serviceType.Id,serviceType.Code}).ToListAsync(cancellationToken);
        var serviceTypeIdsByCode = serviceTypeRecords.ToDictionary(serviceType => serviceType.Code,serviceType => serviceType.Id,StringComparer.OrdinalIgnoreCase);

        var intendedRules = new List<ServiceRequirementRule>
        {
            new()
            {
                PortCallPurpose = PortCallPurpose.BunkerCall,
                ServiceTypeId = GetRequiredId(serviceTypeIdsByCode,"BUNKERING_COORDINATION","service type"),
                RequirementLevel = ServiceRequirementLevel.Mandatory,
                ReadinessImpact = ReadinessImpact.BlocksReadyToLeave,
                DeadlineAnchor = DeadlineAnchor.BeforeEtd,
                DeadlineOffsetHours = 4,
                Rationale = "Bunkering must be completed and confirmed before the vessel is ready to depart.",
                IsActive = true
            },

            new()
            {
                PortCallPurpose = PortCallPurpose.WasteDisposal,
                ServiceTypeId = GetRequiredId(serviceTypeIdsByCode,"GARBAGE_REMOVAL","service type"),
                RequirementLevel = ServiceRequirementLevel.Required,
                ReadinessImpact = ReadinessImpact.BlocksClosure,
                DeadlineAnchor = DeadlineAnchor.BeforeEtd,
                DeadlineOffsetHours = 2,
                Rationale = "Waste services may block closure if unresolved.",
                IsActive = true
            },

            new()
            {
                PortCallPurpose = PortCallPurpose.WasteDisposal,
                ServiceTypeId = GetRequiredId(serviceTypeIdsByCode,"WASTE_DECLARATION_CHECK","service type"),
                RequirementLevel = ServiceRequirementLevel.Mandatory,
                ReadinessImpact = ReadinessImpact.BlocksClosure,
                DeadlineAnchor = DeadlineAnchor.BeforeEtd,
                DeadlineOffsetHours = 2,
                Rationale = "Waste declaration must be checked before final closure.",
                IsActive = true
            },

            new()
            {
                PortCallPurpose = PortCallPurpose.CrewChange,
                ServiceTypeId = GetRequiredId(serviceTypeIdsByCode,"CREW_TRANSPORT","service type"),
                RequirementLevel = ServiceRequirementLevel.Required,
                ReadinessImpact = ReadinessImpact.WarningOnly,
                DeadlineAnchor = DeadlineAnchor.BeforeEtd,
                DeadlineOffsetHours = 3,
                Rationale = "Crew transport delays should be visible to operations staff.",
                IsActive = true
            },

            new()
            {
                PortCallPurpose = PortCallPurpose.TechnicalCall,
                ServiceTypeId = GetRequiredId(serviceTypeIdsByCode,"TECHNICAL_INSPECTION","service type"),
                RequirementLevel = ServiceRequirementLevel.Mandatory,
                ReadinessImpact = ReadinessImpact.BlocksClosure,
                DeadlineAnchor = DeadlineAnchor.BeforeEtd,
                DeadlineOffsetHours = 2,
                Rationale = "Mandatory technical attendance must be checked before closure.",
                IsActive = true
            },

            new()
            {
                PortCallPurpose = PortCallPurpose.CargoOperation,
                ServiceTypeId = GetRequiredId(serviceTypeIdsByCode,"CARGO_OPERATION_STATUS","service type"),
                RequirementLevel = ServiceRequirementLevel.Required,
                ReadinessImpact = ReadinessImpact.WarningOnly,
                DeadlineAnchor = DeadlineAnchor.BeforeEtd,
                DeadlineOffsetHours = 2,
                Rationale = "Cargo operation progress should be visible before departure planning.",
                IsActive = true
            },

            new()
            {
                PortCallPurpose = PortCallPurpose.SupplyCall,
                ServiceTypeId = GetRequiredId(serviceTypeIdsByCode,"FRESH_WATER_SUPPLY","service type"),
                RequirementLevel = ServiceRequirementLevel.Required,
                ReadinessImpact = ReadinessImpact.WarningOnly,
                DeadlineAnchor = DeadlineAnchor.BeforeEtd,
                DeadlineOffsetHours = 3,
                Rationale = "Fresh water supply should be tracked for supply-call readiness.",
                IsActive = true
            },

            new()
            {
                PortCallPurpose = PortCallPurpose.SupplyCall,
                ServiceTypeId = GetRequiredId(serviceTypeIdsByCode,"PROVISIONS_SUPPLY","service type"),
                RequirementLevel = ServiceRequirementLevel.Recommended,
                ReadinessImpact = ReadinessImpact.WarningOnly,
                DeadlineAnchor = DeadlineAnchor.BeforeEtd,
                DeadlineOffsetHours = 3,
                Rationale = "Provisions delivery should be visible to operations staff.",
                IsActive = true
            }
        };

        var existingRuleRecords = await dbContext.ServiceRequirementRules.AsNoTracking().Select(rule => new{rule.PortCallPurpose,rule.ServiceTypeId}).ToListAsync(cancellationToken);
        var existingRuleKeys = existingRuleRecords.Select(rule => (rule.PortCallPurpose, rule.ServiceTypeId)).ToHashSet();
        var missingRules = intendedRules.Where(rule => !existingRuleKeys.Contains((rule.PortCallPurpose, rule.ServiceTypeId))).ToList();

        if (missingRules.Count == 0)
        {
            return;
        }

        dbContext.ServiceRequirementRules.AddRange(missingRules);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static int GetRequiredId(IReadOnlyDictionary<string, int> idsByKey,string key,string entityDescription)
    {
        if (idsByKey.TryGetValue(key, out var id))
        {
            return id;
        }

        throw new InvalidOperationException($"The required {entityDescription} '{key}' was not found while seeding the database.");
    }
}
