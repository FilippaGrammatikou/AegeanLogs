using System;
using System.Collections.Generic;
using System.Text;

namespace AegeanLogs.Domain.Enums;

public enum DeadlineAnchor
{
    BeforeEta,  //before the estimated time of arrival
    AfterEta,  //after the estimated time of arrival
    BeforeEtd,  //before the estimated time of departure
    AfterEtd  //after the estimated time of departure
}
