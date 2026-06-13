using AegeanLogs.Domain.Enums;

namespace AegeanLogs.Application.Common.Security;
public interface ICurrentUserService
{
    int? UserId { get; }
    UserRole? Role { get; }
    int? SupplierId{ get; }
    int? ClientCompanyId { get; }
}
