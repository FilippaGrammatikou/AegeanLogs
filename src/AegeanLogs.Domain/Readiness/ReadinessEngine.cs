using AegeanLogs.Domain.Entities;
using AegeanLogs.Domain.Enums;

namespace AegeanLogs.Domain.Readiness;

public class ReadinessEngine
{
    public ReadinessResult Evaluate(PortCall portCall)
    {
        var blockers = new List<ReadinessBlocker>();

        //Checks service jobs that can block readiness or closure
        foreach (var job in portCall.ServiceJobs)
        {
            if (job.ReadinessImpact is ReadinessImpact.BlocksReadyToLeave
                or ReadinessImpact.BlocksClosure)
            {
                if (job.Status != ServiceJobStatus.Checked)
                {
                    blockers.Add(new ReadinessBlocker
                    {
                        Code = "BlockingServiceJobNotChecked",
                        Message = $"Service job '{job.Title}' is not checked.",
                        Severity = RiskLevel.High,
                        SourceEntityName = nameof(ServiceJob),
                        SourceEntityId = job.Id,
                        IsCritical = true
                    });
                }
            }
        }

        //Check required documents that have not been approved
        foreach (var document in portCall.Documents.Where(d => d.IsRequired))
        {
            if (document.Status != DocumentStatus.Checked)
            {
                blockers.Add(new ReadinessBlocker
                {
                    Code = "RequiredDocumentNotChecked",
                    Message = $"Required document '{document.DocumentType}' is not checked.",
                    Severity = RiskLevel.High,
                    SourceEntityName = nameof(PortCallDocument),
                    SourceEntityId = document.Id,
                    IsCritical = true
                });
            }
        }

        //Reduce readiness score for each blocker
        var score = Math.Max(0, 100 - blockers.Count * 20);

        var riskLevel = score switch
        {
            >= 80 => RiskLevel.Low,
            >= 50 => RiskLevel.Medium,
            >= 25 => RiskLevel.High,
            _ => RiskLevel.Critical
        };

        return new ReadinessResult
        {
            Score = score,
            RiskLevel = riskLevel,
            CanMoveToReadyToLeave = !blockers.Any(b => b.IsCritical),
            CanClose = portCall.Status == PortCallStatus.Departed
                       && !blockers.Any(b => b.IsCritical),
            Blockers = blockers
        };
    }
}
