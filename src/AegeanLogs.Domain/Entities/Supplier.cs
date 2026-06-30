using System;
using System.Collections.Generic;
using System.Text;
using AegeanLogs.Domain.Enums;

namespace AegeanLogs.Domain.Entities;

public class Supplier
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public ServiceCategory ServiceCategory { get; set; }
    public string ContactEmail { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; } = true;

    public List<ServiceJob> ServiceJobs { get; set; } = [];
    public List<ApplicationUser> Users { get; set; } = [];
}
