using AegeanLogs.Application.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AegeanLogs.Infrastructure.Persistence;
internal class EfTransactionManager : ITransactionManager
{
    private readonly AegeanLogsDbContext _dbContext;

    public EfTransactionManager(AegeanLogsDbContext dbContext) {  _dbContext = dbContext; }
    public async Task<T> ExecuteAsync<T>(Func<CancellationToken, Task<T>> operation, CancellationToken cancellationToken = default)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await operation(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
