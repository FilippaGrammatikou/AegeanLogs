using System;
using System.Collections.Generic;
using System.Text;
using AegeanLogs.Domain.Enums;

namespace AegeanLogs.Domain.Entities;

public class PortCallDocument
{
    public int Id { get; set; }
    public int PortCallId { get; set; }
    public PortCall PortCall { get; set; } = null!;
    public string DocumentType { get; set; } = string.Empty;
    public bool IsRequired { get; set; }
    public string? FileName { get; set; }
    public DocumentStatus Status { get; set; } = DocumentStatus.Required;
    public DateTimeOffset? UploadedAt { get; set; }
    
    public int? UploadedByUserId { get; set; }
    public ApplicationUser? UploadedByUser { get; set; }
    public DateTimeOffset? CheckedAt { get; set; }
    public  int? CheckedByUserId { get; set; }
    public ApplicationUser? CheckedByUser { get; set; }
    public string? RejectionReason { get; set; }
}
