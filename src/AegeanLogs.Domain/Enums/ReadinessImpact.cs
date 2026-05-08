using System;
using System.Collections.Generic;
using System.Text;

namespace AegeanLogs.Domain.Enums;

public enum ReadinessImpact
{
    None,
    WarningOnly,  //warning but allow progress
    BlocksReadyToLeave,  //the vessel CANNOT be marked ready to leave
    BlocksClosure
}
