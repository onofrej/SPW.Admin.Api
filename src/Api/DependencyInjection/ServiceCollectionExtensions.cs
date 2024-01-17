using SPW.Admin.Api.Features.Circuit.DataAccess;
using SPW.Admin.Api.Features.User.DataAccess;

namespace SPW.Admin.Api.DependencyInjection;

[ExcludeFromCodeCoverage]
internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection InitializeApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUserData, UserData>();
        services.AddScoped<ICircuitData, CircuitData>();

        return services;
    }
}