using AegeanLogs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AegeanLogs.Domain.Readiness;
public class ReadinessResult
{
    public int Score { get; set; }
    public RiskLevel RiskLevel { get; set; }
    public bool CanMoveToReadyToLeave { get; set; }
    public bool CanClose { get; set;  }
    public List<ReadinessBlocker> Blockers { get; set; } = [];
}
