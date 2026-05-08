using System;
using System.Collections.Generic;
using System.Text;

namespace AegeanLogs.Domain.Enums;

public enum PortCallPurpose
{
    CargoOperation,
    BunkerCall,
    CrewChange,
    TechnicalCall,
    SupplyCall,
    WasteDisposal,
    MultiPurpose  //involving more than one operational purpose
}
