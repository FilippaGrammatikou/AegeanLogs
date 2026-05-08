using System;
using System.Collections.Generic;
using System.Text;
using AegeanLogs.Domain.Enums;

namespace AegeanLogs.Domain.Entities;

public class ServiceType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ServiceCategory Category { get; set; }
    public string? Description { get; set; }
    public bool IsUsuallyRequired { get; set; }
    public bool RequiresSupplier { get; set; }
    public bool RequiredEvidence { get; set; }
    public bool BlockReadinessWhenIncomplete { get; set; }
    public int? DefaultDeadlineHoursNeforeEtd { get; set; }
    public bool isActive { get; set; } = true;
    public List<ServiceJob> ServiceJobs { get; set; } = [];
}
