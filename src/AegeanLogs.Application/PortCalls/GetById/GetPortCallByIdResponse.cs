using AegeanLogs.Domain.Enums;

namespace AegeanLogs.Application.PortCalls.GetById;
public sealed class GetPortCallByIdResponse
{
    public int Id { get; set; }
    public int VesselId { get; set; }
    public string VesselName { get; set; } = string.Empty;
    public string VesselImoNumber { get; set; } = string.Empty;
    public int PortId { get; set; }
    public string PortName { get; set; } = string.Empty;
    public string PortUnLocode { get; set; } = string.Empty;
    public int AssignedAgentUserId { get; set; }
    public string AssignedAgentDisplayName { get; set; } = string.Empty;
    public string AssignedAgentEmail { get; set; } = string.Empty;
    public PortCallPurpose Purpose { get; set; }
    public DateTimeOffset Eta { get; set; }
    public DateTimeOffset Etd { get; set; }
    public DateTimeOffset? ActualArrivalTime { get; set; }
    public DateTimeOffset? ActualDepartureTime { get; set; }
    public PortCallStatus Status { get; set; }
    public string? Notes { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ClosedAt { get; set; }
}
