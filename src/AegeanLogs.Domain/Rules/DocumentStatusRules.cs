using AegeanLogs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AegeanLogs.Domain.Rules;

public static class DocumentStatusRules
{
    public static bool CanMoveTo(
        DocumentStatus currentStatus,
        DocumentStatus nextStatus)
    {
        return currentStatus switch
        {
            DocumentStatus.Required => nextStatus is DocumentStatus.Uploaded or DocumentStatus.Missing,
            DocumentStatus.Uploaded => nextStatus is DocumentStatus.Checked or DocumentStatus.Rejected
                                                             or DocumentStatus.Missing,

            DocumentStatus.Checked => false, //no further progression from this state for this status
            DocumentStatus.Missing => nextStatus is DocumentStatus.Uploaded,
            DocumentStatus.Rejected => nextStatus is DocumentStatus.Uploaded or DocumentStatus.Missing,

            _=> false
        };
    }
}
