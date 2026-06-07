using AegeanLogs.Application.Common.Persistence;
using AegeanLogs.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AegeanLogs.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if(string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");
        }

        services.AddDbContext<AegeanLogsDbContext>(options =>options.UseSqlServer(connectionString));

        services.AddScoped<IAegeanLogsDbContext>(provider => provider.GetRequiredService<AegeanLogsDbContext>());

        return services;
    }
}
