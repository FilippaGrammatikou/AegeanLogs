using AegeanLogs.Domain.Enums;

namespace AegeanLogs.Domain.Readiness;
public class ReadinessResult
{
    public int Score { get; set; }
    public RiskLevel RiskLevel { get; set; }
    public bool CanMoveToReadyToLeave { get; set; }
    public bool CanClose { get; set;  }
    public List<ReadinessBlocker> Blockers { get; set; } = [];
}
