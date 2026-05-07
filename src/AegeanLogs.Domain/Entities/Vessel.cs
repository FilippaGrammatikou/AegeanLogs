using System;
using System.Collections.Generic;
using System.Text;

namespace AegeanLogs.Domain.Entities;

public class Vessel
{
    public int Id { get; set; }
    public int ClientCompanyId { get; set; }
    public int ClientCompany ClientCompany{ get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string ImoNumber { get; set; } = string.Empty;
    public string VesselType { get; set; } string.Empty;
    public string Flag { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public List<PortCall> PortCalls { get; set; } = [];
}
