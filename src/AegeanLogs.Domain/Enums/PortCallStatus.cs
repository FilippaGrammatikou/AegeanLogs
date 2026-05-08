using System;
using System.Collections.Generic;
using System.Text;

namespace AegeanLogs.Domain.Enums;
public enum PortCallStatus
{
    Expected,
    Arrived,
    WorkInProgress,
    ReadyToLeave,
    Departed,
    Closed,
    Cancelled
}
