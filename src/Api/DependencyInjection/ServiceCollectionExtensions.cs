using SPW.Admin.Api.Users.DataAccess;
using SPW.Admin.Api.Users.UseCases.Create;

namespace SPW.Admin.Api.DependencyInjection;

[ExcludeFromCodeCoverage]
internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection InitializeAppliactionServices(this IServiceCollection services)
    {
        services.AddScoped<IDynamoDBContext, DynamoDBContext>();
        services.AddScoped<IUserData, UserData>();
        services.AddAWSService<IAmazonDynamoDB>();

        services.AddCreateUserUseCase();

        return services;
    }

    private static IServiceCollection AddCreateUserUseCase(this IServiceCollection services)
    {
        services.AddScoped<IUseCase, UseCase>();

        return services;
    }
}