using System;
using System.Collections.Generic;
using System.Text;
using AegeanLogs.Domain.Enums;

namespace AegeanLogs.Domain.Entities;

public class ServiceRequirementRule
{
    public int Id { get; set; }
    public PortCallPurpose PortCallPurpose { get; set; }
    public int ServiceTypeId { get; set; }
    public ServiceType ServiceType { get; set; } = null!;
    public ServiceRequirementLevel RequirementLevel { get; set; }
    public ReadinessImpact ReadinessImpact { get; set; }
    public DeadlineAnchor DeadlineAnchor { get; set; }
    public int DeadlineOffsetHours { get; set; }
    public string? Rationale { get; set; }
    public bool IsActive { get; set; } = true;
}
