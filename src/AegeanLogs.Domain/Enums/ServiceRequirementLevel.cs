using System;
using System.Collections.Generic;
using System.Text;

namespace AegeanLogs.Domain.Enums;

public enum ServiceRequirementLevel
{
    Optional,
    Recommended,
    Required,  //expected for this kind of port cal
    Mandatory  //cannot be skipped without manager override
}
