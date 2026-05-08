using System;
using System.Collections.Generic;
using System.Text;

namespace AegeanLogs.Domain.Enums;
public enum ServiceJobStatus
{
    Requested,
    Assigned,
    InProgress,
    Done,
    Checked,
    Blocked,
    Late,
    Cancelled
}
