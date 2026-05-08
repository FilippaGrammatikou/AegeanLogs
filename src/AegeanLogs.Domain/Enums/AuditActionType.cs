using System;
using System.Collections.Generic;
using System.Text;

namespace AegeanLogs.Domain.Enums;
public enum AuditActionType
{
    PortCallCreated,
    PortCallStatusChanged,
    ServiceJobCreated,
    SupplierAssigned,
    ServiceJobStatusChanged,
    DocumentCreated,
    DocumentUploaded,
    DocumentChecked,
    DocumentRejected,
    ReadinessEvaluated,
    PortCallClosed,
    PortCallCancelled
}
