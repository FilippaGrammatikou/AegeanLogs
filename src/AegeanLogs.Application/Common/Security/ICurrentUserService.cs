namespace AegeanLogs.Application.Common.Security;

public interface ICurrentUserService
{
    int? UserId { get; }
    string? Role { get; }
    int? SupplierId{ get; }
    int? ClientCompanyId { get; }
}
