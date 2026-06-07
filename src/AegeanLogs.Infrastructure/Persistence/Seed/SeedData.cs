using AegeanLogs.Domain.Entities;
using AegeanLogs.Domain.Enums;

namespace AegeanLogs.Infrastructure.Persistence.Seed;
public static class SeedData
{
    public static List<Port> Ports()
    {
        return
        [
            new Port
            {Name = "Piraeus", Country = "Greece", UnLocode = "GRPIR", IsActive = true},

            new Port
            {Name = "Elefsina", Country = "Greece", UnLocode = "GRELE", IsActive = true},

            new Port
            {Name = "Lavrio", Country = "Greece", UnLocode = "GRLAV",IsActive = true},

            new Port
            {Name = "Thessaloniki", Country = "Greece", UnLocode = "GRSKG", IsActive = true},

            new Port
            {Name = "Heraklion", Country = "Greece", UnLocode = "GRHER", IsActive = true}
        ];
    }

    public static List<ServiceType> ServiceTypes()
    {
        return
        [
            new ServiceType
            {
                Code = "PILOTAGE_COORDINATION",
                Name = "Pilotage coordination",
                Category = ServiceCategory.Nautical,
                Description = "Coordination of pilotage requirements for vessel movement.",
                RequiresExternalSupplier = true,
                RequiresCompletionEvidence = false,
                IsActive = true},

            new ServiceType
            {
                Code = "TOWAGE_COORDINATION",
                Name = "Towage coordination",
                Category = ServiceCategory.Nautical,
                Description = "Coordination of tug support for arrival, shifting, or departure.",
                RequiresExternalSupplier = true,
                RequiresCompletionEvidence = false,
                IsActive = true},

            new ServiceType
            {
                Code = "FRESH_WATER_SUPPLY",
                Name = "Fresh water supply",
                Category = ServiceCategory.VesselSupply,
                Description = "Arrangement of fresh water supply during port stay.",
                RequiresExternalSupplier = true,
                RequiresCompletionEvidence = true,
                IsActive = true},

            new ServiceType
            {
                Code = "PROVISIONS_SUPPLY",
                Name = "Provisions supply",
                Category = ServiceCategory.VesselSupply,
                Description = "Arrangement of vessel provisions delivery.",
                RequiresExternalSupplier = true,
                RequiresCompletionEvidence = true,
                IsActive = true},

            new ServiceType
            {
                Code = "BUNKERING_COORDINATION",
                Name = "Bunkering coordination",
                Category = ServiceCategory.VesselSupply,
                Description = "Coordination of bunkering supplier, timing, and completion evidence.",
                RequiresExternalSupplier = true,
                RequiresCompletionEvidence = true,
                IsActive = true},

            new ServiceType
            {
                Code = "GARBAGE_REMOVAL",
                Name = "Garbage removal",
                Category = ServiceCategory.WasteAndEnvironmental,
                Description = "Coordination of garbage collection from vessel.",
                RequiresExternalSupplier = true,
                RequiresCompletionEvidence = true,
                IsActive = true},

            new ServiceType
            {
                Code = "SLUDGE_DISPOSAL",
                Name = "Sludge disposal",
                Category = ServiceCategory.WasteAndEnvironmental,
                Description = "Coordination of sludge disposal service.",
                RequiresExternalSupplier = true,
                RequiresCompletionEvidence = true,
                IsActive = true},

            new ServiceType
            {
                Code = "CREW_TRANSPORT",
                Name = "Crew transport",
                Category = ServiceCategory.Crew,
                Description = "Transport coordination for crew movement.",
                RequiresExternalSupplier = true,
                RequiresCompletionEvidence = false,
                IsActive = true},

            new ServiceType
            {
                Code = "TECHNICAL_INSPECTION",
                Name = "Technical inspection",
                Category = ServiceCategory.Technical,
                Description = "Coordination of technical inspection or minor repair attendance.",
                RequiresExternalSupplier = true,
                RequiresCompletionEvidence = true,
                IsActive = true},

            new ServiceType
            {
                Code = "DEPARTURE_CLEARANCE_SUPPORT",
                Name = "Departure clearance support",
                Category = ServiceCategory.Documentation,
                Description = "Follow-up of departure clearance readiness.",
                RequiresExternalSupplier = false,
                RequiresCompletionEvidence = false,
                IsActive = true},

            new ServiceType
            {
                Code = "WASTE_DECLARATION_CHECK",
                Name = "Waste declaration check",
                Category = ServiceCategory.Documentation,
                Description = "Check of waste declaration status before closure.",
                RequiresExternalSupplier = false,
                RequiresCompletionEvidence = false,
                IsActive = true},

            new ServiceType
            {
                Code = "CARGO_OPERATION_STATUS",
                Name = "Cargo operation status update",
                Category = ServiceCategory.CargoCoordination,
                Description = "Monitoring and updating cargo operation progress.",
                RequiresExternalSupplier = false,
                RequiresCompletionEvidence = false,
                IsActive = true}
        ];
    }

    public static List<ClientCompany> ClientCompanies()
    {
        return
        [
            new ClientCompany
            {
                Name = "Aegean Blue Shipping Ltd",
                ContactEmail = "ops@aegeanblue.example",
                PhoneNumber = "+30 210 000 1001",
                IsActive = true},

            new ClientCompany
            {
                Name = "Hellenic Bulk Operators",
                ContactEmail = "operations@hellenicbulk.example",
                PhoneNumber = "+30 210 000 1002",
                IsActive = true}
        ];
    }

    public static List<Supplier> Suppliers()
    {
        return
        [
            new Supplier
            {
                Name = "Piraeus Marine Supplies",
                ServiceCategory = ServiceCategory.VesselSupply,
                ContactEmail = "dispatch@piraeusmarinesupplies.example",
                PhoneNumber = "+30 210 000 2001",
                IsActive = true},

            new Supplier
            {
                Name = "Attica Waste Services",
                ServiceCategory = ServiceCategory.WasteAndEnvironmental,
                ContactEmail = "ops@atticawaste.example",
                PhoneNumber = "+30 210 000 2002",
                IsActive = true},

            new Supplier
            {
                Name = "Aegean Crew Transport",
                ServiceCategory = ServiceCategory.Crew,
                ContactEmail = "booking@aegeancrewtransport.example",
                PhoneNumber = "+30 210 000 2003",
                IsActive = true},

            new Supplier
            {
                Name = "Hellas Technical Marine",
                ServiceCategory = ServiceCategory.Technical,
                ContactEmail = "service@hellastechmarine.example",
                PhoneNumber = "+30 210 000 2004",
                IsActive = true}
        ];
    }
}
