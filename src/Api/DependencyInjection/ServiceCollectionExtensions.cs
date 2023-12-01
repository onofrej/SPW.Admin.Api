using SPW.Admin.Api.Features.Users.DataAccess;

namespace SPW.Admin.Api.DependencyInjection;

[ExcludeFromCodeCoverage]
internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection InitializeApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IDynamoDBContext, DynamoDBContext>();
        services.AddScoped<IUserData, UserData>();
        services.AddAWSService<IAmazonDynamoDB>();

        return services;
    }
}