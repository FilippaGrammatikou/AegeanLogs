using System;
using System.Collections.Generic;
using System.Text;

namespace AegeanLogs.Domain.Entities;

public class ApplicationUser
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set;  } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public int? Supplier { get; set; }
    public int? ClientCompanyId { get; set; }
    public ClientCompany? ClientCompany { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
