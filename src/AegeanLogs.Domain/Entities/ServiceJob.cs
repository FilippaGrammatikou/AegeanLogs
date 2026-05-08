using System;
using System.Collections.Generic;
using System.Text;
using AegeanLogs.Domain.Enums;

namespace AegeanLogs.Domain.Entities;

public class ServiceJob
{
    public int Id { get; set; }
    public int PortCallId { get; set; }
    public PortCall PortCall { get; set; } = null!;
    public int ServiceTypeId { get; set; }
    public ServiceType ServiceType { get; set; } = null!;
    public int? SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }


    public ServiceRequirementLevel RequirementLevel { get; set; }
    public ReadinessImpact ReadinessImpact { get; set; }

    public DateTimeOffset Deadline { get; set; }
    public ServiceJobStatus Status { get; set; } = ServiceJobStatus.Requested;
    public string? SupplierNotes { get; set; }
    public string? EvidenceFileName { get; set; }

    public DateTimeOffset? StartedAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
    public DateTimeOffset? CheckedAt { get; set; }

    public int? CheckedByUserId { get; set; }
    public ApplicationUser? CheckedByUser { get; set; }
}
