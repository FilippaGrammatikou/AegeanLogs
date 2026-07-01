using System;
using System.Collections.Generic;
using System.Text;

namespace AegeanLogs.Application.Common.Persistence;
public interface ITransactionManager
{
    Task<T> ExecuteAsync<T>(Func<CancellationToken, Task<T>> operation, CancellationToken cancellationToken = default);
}
