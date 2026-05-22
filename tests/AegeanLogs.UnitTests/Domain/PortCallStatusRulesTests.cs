using AegeanLogs.Domain.Enums;
using AegeanLogs.Domain.Rules;

namespace AegeanLogs.UnitTests.Domain;

public class PortCallStatusRulesTests
{
    [Fact]
    public void CanMoveTo_WhenExpectedToArrived_ReturnsTrue()
    {
        var result = PortCallStatusRules.CanMoveTo(PortCallStatus.Expected, PortCallStatus.Arrived);
        Assert.True(result);
    }

    [Fact]
    public void CanMoveTo_WhenExpectedToClose_ReturnsFalse()
    {
        var result = PortCallStatusRules.CanMoveTo(PortCallStatus.Expected, PortCallStatus.Closed);
        Assert.False(result);
    }

    [Fact]
    public void CanMoveTo_WhenWorkInProgressToReady_ReturnsTrue()
    {
        var result = PortCallStatusRules.CanMoveTo(PortCallStatus.WorkInProgress, PortCallStatus.ReadyToLeave);
        Assert.True(result);
    }

    [Fact]
    public void CanMoveTo_WhenClosedToArrived_ReturnsFalse()
    {
        var result = PortCallStatusRules.CanMoveTo(PortCallStatus.Closed, PortCallStatus.Arrived);
        Assert.False(result);
    }
}
