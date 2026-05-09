using System;
using System.Collections.Generic;
using System.Text;
using AegeanLogs.Domain.Enums;

namespace AegeanLogs.Domain.Rules;
public static class PortCallStatusRules
{
    public static bool CanMoveTo(
        PortCallStatus currentStatus,
        PortCallStatus nextStatus)
    {
        return currentStatus switch
        {
            PortCallStatus.Expected => nextStatus is PortCallStatus.Arrived or PortCallStatus.Cancelled,
            PortCallStatus.Arrived => nextStatus is PortCallStatus.WorkInProgress or PortCallStatus.Cancelled,
            PortCallStatus.WorkInProgress => nextStatus is PortCallStatus.ReadyToLeave or PortCallStatus.Cancelled,
            PortCallStatus.ReadyToLeave => nextStatus is PortCallStatus.Departed,
            PortCallStatus.Departed => nextStatus is PortCallStatus.Closed,

            PortCallStatus.Closed => false,  //no further progression from this state for this port call status
            PortCallStatus.Cancelled => false,  //no further progression from this state for this port call status
            _ => false
        };
    }
}
