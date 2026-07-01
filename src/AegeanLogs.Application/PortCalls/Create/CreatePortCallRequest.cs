using AegeanLogs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AegeanLogs.Application.PortCalls.Create;
public sealed class CreatePortCallRequest
{
    public int VesselId { get; set; }
    public int PortId { get; set; }
    public PortCallPurpose Purpose{ get; set; }
    public DateTimeOffset Eta { get; set; }
    public DateTimeOffset Etd { get; set; }
    public int AssignedAgentUserId { get; set; }
    public string? Notes { get; set; }
}
