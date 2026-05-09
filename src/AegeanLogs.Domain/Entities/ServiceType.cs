using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using AegeanLogs.Domain.Enums;

namespace AegeanLogs.Domain.Entities;

public class ServiceType
{
    public int Id { get; set; }
    public string Code { get; set;  } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public ServiceCategory Category { get; set; }
    public string? Description { get; set; }

    public bool RequiresExternalSupplier { get; set; }
    public bool RequiresCompletionEvidence { get; set; }

 
    public bool IsActive { get; set; } = true;

    public List<ServiceJob> ServiceJobs { get; set; } = [];
    public List<ServiceRequirementRule> RequirementRules { get; set; } = []; 
}
