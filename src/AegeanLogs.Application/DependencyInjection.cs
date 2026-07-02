using AegeanLogs.Application.PortCalls.Create;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace AegeanLogs.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreatePortCallRequest>, CreatePortCallValidator>();
        services.AddScoped<CreatePortCallService>();
        return services;
    }
}
