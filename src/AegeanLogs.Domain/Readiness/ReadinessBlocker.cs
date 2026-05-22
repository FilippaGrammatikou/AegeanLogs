using AegeanLogs.Domain.Enums;

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
