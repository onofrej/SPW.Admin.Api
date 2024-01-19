using SPW.Admin.Api.Features.Circuit.DataAccess;
using SPW.Admin.Api.Features.Point.DataAccess;
using SPW.Admin.Api.Features.User.DataAccess;
using SPW.Admin.Api.Features.Validity.DataAcces;

namespace SPW.Admin.Api.DependencyInjection;

[ExcludeFromCodeCoverage]
internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection InitializeApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUserData, UserData>();
        services.AddScoped<ICircuitData, CircuitData>();
        services.AddScoped<IPointData, PointData>();
        services.AddScoped<IValidtyData, ValidityData>();

        return services;
    }
}