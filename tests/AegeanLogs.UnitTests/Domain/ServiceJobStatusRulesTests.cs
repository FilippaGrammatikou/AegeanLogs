using AegeanLogs.Domain.Enums;
using AegeanLogs.Domain.Rules;

namespace AegeanLogs.UnitTests.Domain;

public class ServiceJobStatusRulesTests
{
    [Fact]
    public void CanMoveTo_WhenRequestToAssigned_ReturnsTrue()
    {
        var result = ServiceJobStatusRules.CanMoveTo(ServiceJobStatus.Requested, ServiceJobStatus.Assigned);
        Assert.True(result);
    }
        
   [Fact]
   public void CanMoveTo_WhenRequestedToChecked_ReturnsFalse()
   {
      var result = ServiceJobStatusRules.CanMoveTo(ServiceJobStatus.Requested, ServiceJobStatus.Checked);
      Assert.False(result);
   }

    [Fact]
    public void CanMoveTo_WhenDoneToChecked_ReturnTrue()
    {
        var result = ServiceJobStatusRules.CanMoveTo(ServiceJobStatus.Done, ServiceJobStatus.Checked);
        Assert.True(result);
    }

    [Fact]
    public void CanMoveTo_WhenCheckedToInProgress_ReturnFalse()
    {
        var result = ServiceJobStatusRules.CanMoveTo(ServiceJobStatus.Checked, ServiceJobStatus.InProgress);
            Assert.False(result);
    }
}
