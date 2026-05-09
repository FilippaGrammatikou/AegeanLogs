using System;
using System.Collections.Generic;
using System.Text;
using AegeanLogs.Domain.Enums;

namespace AegeanLogs.Domain.Entities;

public class AuditLogEntry
{
    public int Id { get; set; }
    public int PortCallId { get; set; }
    public PortCall PortCall { get; set; } = null!;
    public int? UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public AuditActionType ActionType { get; set; }
    public string EntityName { get; set; } = string.Empty;
    public int EntityId { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public string Summary { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
