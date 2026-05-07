using System;
using System.Collections.Generic;
using System.Text;

namespace AegeanLogs.Domain.Entities;

public class Port
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { set; get;  } = string.Empty;
    public string UnLocode { get; set;  } = string.Empty;
    public bool IsActive { get; set; } = true;
    public List<PortCall> PortCalls { get; set; } = [];
}
