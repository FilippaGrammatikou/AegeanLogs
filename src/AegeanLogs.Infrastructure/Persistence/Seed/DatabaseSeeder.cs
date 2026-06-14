using AegeanLogs.Domain.Entities;
using AegeanLogs.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AegeanLogs.Infrastructure.Persistence.Seed;
public static class DatabaseSeeder
{
    public static async Task SeedAsync(AegeanLogsDbContext dbContext, CancellationToken cancellationToken = default)
    {
        await SeedPortsAsync(dbContext, cancellationToken);
        await SeedServiceTypesAsync(dbContext, cancellationToken);
        await SeedClientCompaniesAndVesselsAsync(dbContext, cancellationToken);
        await SeedClientCompaniesAndVesselsAsync(dbContext, cancellationToken);
        await SeedSuppliersAsync(dbContext, cancellationToken);
        await SeedUsersAsync(dbContext, cancellationToken);
        await SeedServiceRequirementRulesAsync(dbContext, cancellationToken);
    }

    private static async Task SeedPortsAsync(AegeanLogsDbContext dbContext, CancellationToken cancellationToken)
    {
        var existingUnLocodes = await dbContext.Ports.AsNoTracking().Select(port => port.UnLocode).ToListAsync(cancellationToken);
        var existingUnLocodeSet = existingUnLocodes.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var missingPorts = SeedData.Ports().Where(port=> !existingUnLocodeSet.Contains(port.UnLocode)).ToList();

        if (missingPorts.Count == 0)
        {
            return;
        }

        dbContext.Ports.AddRange(missingPorts);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task SeedServiceTypesAsync(AegeanLogsDbContext dbContext, CancellationToken cancellationToken)
    {

        var existingCodes = await dbContext.ServiceTypes.AsNoTracking().Select(serviceType => serviceType.Code).ToListAsync(cancellationToken);
        var existingCodeSet = existingCodes.ToHashSet(StringComparer.OrdinalIgnoreCase);
        var missingServiceTypes = SeedData.ServiceTypes().Where(serviceType=> !existingCodeSet.Contains(serviceType.Code)).ToList();

        if(missingServiceTypes.Count == 0)
        {
            return;
        }

        dbContext.ServiceTypes.AddRange(missingServiceTypes);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task SeedClientCompaniesAndVesselsAsync(AegeanLogsDbContext dbContext, CancellationToken cancellationToken)
    {
        if (await dbContext.ClientCompanies.AnyAsync(cancellationToken))
        {
            return;
        }

        var clientCompanies = SeedData.ClientCompanies();
        dbContext.ClientCompanies.AddRange(clientCompanies);
        await dbContext.SaveChangesAsync(cancellationToken);
        var aegeanBlue = await dbContext.ClientCompanies.SingleAsync(company => company.Name == "Aegean Blue Shipping Ltd", cancellationToken);
        var hellenicBulk = await dbContext.ClientCompanies.SingleAsync(company => company.Name == "Hellenic Bulk Operators", cancellationToken);

        var vessels = new List<Vessel>
        {
            new()
            {
                ClientCompanyId = aegeanBlue.Id,
                Name = "MV Aegean Star",
                ImoNumber = "IMO9300001",
                VesselType = "Container Ship",
                Flag = "Greece",
                IsActive = true},

            new()
            {
                ClientCompanyId = aegeanBlue.Id,
                Name = "MV Cyclades Trader",
                ImoNumber = "IMO9300002",
                VesselType = "Bulk Carrier",
                Flag = "Malta",
                IsActive = true},

            new()
            {
                ClientCompanyId = hellenicBulk.Id,
                Name = "MV Thermaikos",
                ImoNumber = "IMO9300003",
                VesselType = "Bulk Carrier",
                Flag = "Greece",
                IsActive = true}
        };

        dbContext.Vessels.AddRange(vessels);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task SeedSuppliersAsync(AegeanLogsDbContext dbContext, CancellationToken cancellationToken)
    {
        if (await dbContext.Suppliers.AnyAsync(cancellationToken))
        {
            return;
        }

        dbContext.Suppliers.AddRange(SeedData.Suppliers());
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task SeedUsersAsync(AegeanLogsDbContext dbContext, CancellationToken cancellationToken)
    {
        if (await dbContext.ApplicationUsers.AnyAsync(cancellationToken))
        {
            return;
        }

        var clientCompany = await dbContext.ClientCompanies.SingleAsync(company => company.Name == "Aegean Blue Shipping Ltd", cancellationToken);
        var supplier = await dbContext.Suppliers.SingleAsync(supplier => supplier.Name == "Piraeus Marine Supplies", cancellationToken);

        var users = new List<ApplicationUser>
        {
            new()
            {
                Email = "admin@aegeanlogs.local",
                DisplayName = "Demo Admin",
                PasswordHash = "DEV_ONLY_PASSWORD_HASH_NOT_REAL",
                Role = UserRole.Admin,
                IsActive = true},

            new()
            {
                Email = "agent@aegeanlogs.local",
                DisplayName = "Demo Port Agent",
                PasswordHash = "DEV_ONLY_PASSWORD_HASH_NOT_REAL",
                Role = UserRole.PortAgent,
                IsActive = true},

            new()
            {
                Email = "docs@aegeanlogs.local",
                DisplayName = "Demo Documentation Officer",
                PasswordHash = "DEV_ONLY_PASSWORD_HASH_NOT_REAL",
                Role = UserRole.DocumentationOfficer,
                IsActive = true},

            new()
            {
                Email = "manager@aegeanlogs.local",
                DisplayName = "Demo Operations Manager",
                PasswordHash = "DEV_ONLY_PASSWORD_HASH_NOT_REAL",
                Role = UserRole.OperationsManager,
                IsActive = true},

            new()
            {
                Email = "supplier@aegeanlogs.local",
                DisplayName = "Demo Supplier User",
                PasswordHash = "DEV_ONLY_PASSWORD_HASH_NOT_REAL",
                Role = UserRole.SupplierUser,
                SupplierId = supplier.Id,
                IsActive = true},

            new()
            {
                Email = "client@aegeanlogs.local",
                DisplayName = "Demo Client User",
                PasswordHash = "DEV_ONLY_PASSWORD_HASH_NOT_REAL",
                Role = UserRole.ClientUser,
                ClientCompanyId = clientCompany.Id,
                IsActive = true}
        };

        dbContext.ApplicationUsers.AddRange(users);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task SeedServiceRequirementRulesAsync(AegeanLogsDbContext dbContext, CancellationToken cancellationToken)
    {
        if (await dbContext.ServiceRequirementRules.AnyAsync(cancellationToken))
        {
            return;
        }

        var serviceTypes = await dbContext.ServiceTypes.ToDictionaryAsync(serviceType => serviceType.Code, cancellationToken);

        var rules = new List<ServiceRequirementRule>
        {
            new()
            {
                PortCallPurpose = PortCallPurpose.BunkerCall,
                ServiceTypeId = serviceTypes["BUNKERING_COORDINATION"].Id,
                RequirementLevel = ServiceRequirementLevel.Mandatory,
                ReadinessImpact = ReadinessImpact.BlocksReadyToLeave,
                DeadlineAnchor = DeadlineAnchor.BeforeEtd,
                DeadlineOffsetHours = 4,
                Rationale = "Bunkering must be completed and confirmed before the vessel is ready to depart.",
                IsActive = true},

            new()
            {
                PortCallPurpose = PortCallPurpose.WasteDisposal,
                ServiceTypeId = serviceTypes["GARBAGE_REMOVAL"].Id,
                RequirementLevel = ServiceRequirementLevel.Required,
                ReadinessImpact = ReadinessImpact.BlocksClosure,
                DeadlineAnchor = DeadlineAnchor.BeforeEtd,
                DeadlineOffsetHours = 2,
                Rationale = "Waste services may block closure if unresolved.",
                IsActive = true},

            new()
            {
                PortCallPurpose = PortCallPurpose.WasteDisposal,
                ServiceTypeId = serviceTypes["WASTE_DECLARATION_CHECK"].Id,
                RequirementLevel = ServiceRequirementLevel.Mandatory,
                ReadinessImpact = ReadinessImpact.BlocksClosure,
                DeadlineAnchor = DeadlineAnchor.BeforeEtd,
                DeadlineOffsetHours = 2,
                Rationale = "Waste declaration must be checked before final closure.",
                IsActive = true},

            new()
            {
                PortCallPurpose = PortCallPurpose.CrewChange,
                ServiceTypeId = serviceTypes["CREW_TRANSPORT"].Id,
                RequirementLevel = ServiceRequirementLevel.Required,
                ReadinessImpact = ReadinessImpact.WarningOnly,
                DeadlineAnchor = DeadlineAnchor.BeforeEtd,
                DeadlineOffsetHours = 3,
                Rationale = "Crew transport delays should be visible to operations staff.",
                IsActive = true},

            new()
            {
                PortCallPurpose = PortCallPurpose.TechnicalCall,
                ServiceTypeId = serviceTypes["TECHNICAL_INSPECTION"].Id,
                RequirementLevel = ServiceRequirementLevel.Mandatory,
                ReadinessImpact = ReadinessImpact.BlocksClosure,
                DeadlineAnchor = DeadlineAnchor.BeforeEtd,
                DeadlineOffsetHours = 2,
                Rationale = "Mandatory technical attendance must be checked before closure.",
                IsActive = true},

            new()
            {
                PortCallPurpose = PortCallPurpose.CargoOperation,
                ServiceTypeId = serviceTypes["CARGO_OPERATION_STATUS"].Id,
                RequirementLevel = ServiceRequirementLevel.Required,
                ReadinessImpact = ReadinessImpact.WarningOnly,
                DeadlineAnchor = DeadlineAnchor.BeforeEtd,
                DeadlineOffsetHours = 2,
                Rationale = "Cargo operation progress should be visible before departure planning.",
                IsActive = true},

            new()
            {
                PortCallPurpose = PortCallPurpose.SupplyCall,
                ServiceTypeId = serviceTypes["FRESH_WATER_SUPPLY"].Id,
                RequirementLevel = ServiceRequirementLevel.Required,
                ReadinessImpact = ReadinessImpact.WarningOnly,
                DeadlineAnchor = DeadlineAnchor.BeforeEtd,
                DeadlineOffsetHours = 3,
                Rationale = "Fresh water supply should be tracked for supply-call readiness.",
                IsActive = true},

            new()
            {
                PortCallPurpose = PortCallPurpose.SupplyCall,
                ServiceTypeId = serviceTypes["PROVISIONS_SUPPLY"].Id,
                RequirementLevel = ServiceRequirementLevel.Recommended,
                ReadinessImpact = ReadinessImpact.WarningOnly,
                DeadlineAnchor = DeadlineAnchor.BeforeEtd,
                DeadlineOffsetHours = 3,
                Rationale = "Provisions delivery should be visible to operations staff.",
                IsActive = true}
        };

        dbContext.ServiceRequirementRules.AddRange(rules);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
