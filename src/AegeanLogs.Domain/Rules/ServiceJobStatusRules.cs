using System;
using System.Collections.Generic;
using System.Text;
using AegeanLogs.Domain.Enums;

namespace AegeanLogs.Domain.Rules;
public static class ServiceJobStatusRules
{
    public static bool CanMoveTo(
        ServiceJobStatus currentStatus,
        ServiceJobStatus nextStatus)
    {
        return currentStatus switch
        {
            ServiceJobStatus.Requested => nextStatus is ServiceJobStatus.Assigned
                                                                or ServiceJobStatus.Blocked
                                                                or ServiceJobStatus.Cancelled,

            ServiceJobStatus.Assigned => nextStatus is ServiceJobStatus.InProgress or ServiceJobStatus.Blocked
                                                              or ServiceJobStatus.Late
                                                              or ServiceJobStatus.Cancelled,

            ServiceJobStatus.InProgress => nextStatus is ServiceJobStatus.Done or ServiceJobStatus.Blocked
                                                                or ServiceJobStatus.Late
                                                                or ServiceJobStatus.Cancelled,

            ServiceJobStatus.Done => nextStatus is ServiceJobStatus.Checked
                                                       or ServiceJobStatus.Blocked,
            
            ServiceJobStatus.Checked =>false,//no further progression from this state for this service jobs status

            ServiceJobStatus.Blocked => nextStatus is ServiceJobStatus.Assigned
                                                            or ServiceJobStatus.InProgress
                                                            or ServiceJobStatus.Cancelled,

            ServiceJobStatus.Late => nextStatus is ServiceJobStatus.InProgress
                                                      or ServiceJobStatus.Done
                                                      or ServiceJobStatus.Cancelled,

            ServiceJobStatus.Cancelled => false, //no further progression from this state for this service jobs status
            _ => false
        };
    }
}
