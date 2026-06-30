using System;
using System.Collections.Generic;
using System.Text;

namespace AegeanLogs.Domain.Entities;

public class ClientCompany
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; } = true;
    public List<Vessel> Vessels { get; set; } = [];
    public List<ApplicationUser> Users { get; set; } = [];
}
