using AegeanLogs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AegeanLogs.Application.PortCalls.Create;
public sealed class CreatePortCallResponse
{
    public int Id {  get; set; }
    public int VesselId { get; set; }
    public int PortId { get; set; }
    public PortCallPurpose Purpose { get; set; }
    public DateTimeOffset Eta { get; set; }
    public DateTimeOffset Etd { get; set; }
    public int AssignedAgentUserId { get; set; }
    public PortCallStatus Status { get; set; }
    public string? Notes { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
