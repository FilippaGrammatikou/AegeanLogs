using AegeanLogs.Domain.Entities;
using AegeanLogs.Domain.Enums;
using AegeanLogs.Domain.Readiness;

namespace AegeanLogs.UnitTests.Domain;

public class ReadinessEngineTests
{
    [Fact]
    public void Evaluate_WhenBlockingServiceJobIsNotChecked_ReturnsCriticalBlocker()
    {
        var portCall = new PortCall
        {
            Status = PortCallStatus.WorkInProgress,
            ServiceJobs =
            [
                new ServiceJob
                {
                    Id = 10,
                    Title = "Waste Collection",
                    ReadinessImpact = ReadinessImpact.BlocksReadyToLeave,
                    Status = ServiceJobStatus.Done
                }
            ]
        };

        var engine = new ReadinessEngine();
        var result = engine.Evaluate( portCall );

        Assert.False(result.CanMoveToReadyToLeave);
        Assert.Contains(result.Blockers, blocker => blocker.Code == "BlockingServiceJobNotChecked" && blocker.SourceEntityId == 10);
    }

    [Fact]
    public void Evaluate_WhenRequiredDocumentIsNotChecked_ReturnsCriticalBlocker()
    {
        var portCall = new PortCall
        {
            Status = PortCallStatus.WorkInProgress,
            Documents =
            [
                new PortCallDocument
                {
                    Id = 20,
                    DocumentType = "Departure Clearance",
                    IsRequired = true,
                    Status = DocumentStatus.Uploaded
                }
             ]
        };

        var engine = new ReadinessEngine();
        var result = engine.Evaluate(portCall);

        Assert.False(result.CanMoveToReadyToLeave);
        Assert.Contains(result.Blockers, blocker => blocker.Code == "RequiredDocumentNotChecked" && blocker.SourceEntityId == 20);
    }
}
