using System;
using System.Collections.Generic;
using System.Text;
using AegeanLogs.Domain.Enums;

namespace AegeanLogs.Domain.Entities;

internal class PortCall
{
    public int Id { get; set; }
    public int VesselId { get; set; }
    public Vessel Vessel { get; set; }
    public int PortId { get; set; }
    public Port Port { get; set; } = null!;
    public int AssignedAgentUserId { get; set; }
    public ApplicationUser AssignedAgent {  get; set; } = null!;
    public PortalCallPurpose Purpose { get; set; }
    public DateTimeOffset Eta { get; set; }
    public DateTimeOffset Etd { get; set; }
    public DateTimeOffset? ActualArrivalTime { get; set; }
    public DateTimeOffset? ActualDepartureTime { get; set; }
    public PortCallStatus Status { get; set; } = PortCallStatus.Expected;
    public string? Notes { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ClosedAt { get; set; }
    public List<ServiceJob> ServiceJobs { get; set; } = [];
    public List<PortCallDocument> Documents { get; set; } = [];
    public List<AuditLogEntry> AuditLogEntries {  get; set; } = [];
}
