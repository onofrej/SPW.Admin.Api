using SPW.Admin.Api.Features.User.DataAccess;
using SPW.Admin.Api.Features.User.Get;
using SPW.Admin.Api.Features.User.Delete;
using SPW.Admin.Api.Features.User.Put;

namespace SPW.Admin.Api.DependencyInjection;

[ExcludeFromCodeCoverage]
internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection InitializeApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUserData, UserData>();
        services.AddScoped<HandlerGet>();
        services.AddScoped<HandlerDelete>();
        services.AddScoped<HandlerPut>();

        return services;
    }
}