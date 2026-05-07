using System;
using System.Collections.Generic;
using System.Text;

namespace AegeanLogs.Domain.Entities;

public class ClientCompany
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ContactEmail { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; } = true;
    public List<Vessel> Vessels { get; set; } = [];
    public List<ApplicationUser> Users { get; set; } = [];
}
