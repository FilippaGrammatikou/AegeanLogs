using AegeanLogs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AegeanLogs.Domain.Readiness;
public class ReadinessBlocker
{
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public RiskLevel Severity { get; set; }
    public string SourceEntityName { get; set; } = string.Empty;
    public int? SourceEntityId { get; set; }
    public bool IsCritical { get; set; }
}
