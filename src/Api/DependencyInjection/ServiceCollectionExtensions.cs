using SPW.Admin.Api.Features.Announcement;
using SPW.Admin.Api.Features.Authentication;
using SPW.Admin.Api.Features.Circuit;
using SPW.Admin.Api.Features.Domain;
using SPW.Admin.Api.Features.Holiday;
using SPW.Admin.Api.Features.Point;
using SPW.Admin.Api.Features.Schedule;
using SPW.Admin.Api.Features.SpecialDay;
using SPW.Admin.Api.Features.User;
using SPW.Admin.Api.Features.Validity;

namespace SPW.Admin.Api.DependencyInjection;

[ExcludeFromCodeCoverage]
internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection InitializeApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<IAnnouncementData, AnnouncementData>();
        services.AddScoped<IAuthenticationData, AuthenticationData>();
        services.AddScoped<ICircuitData, CircuitData>();
        services.AddScoped<IDomainData, DomainData>();
        services.AddScoped<IHolidayData, HolidayData>();
        services.AddScoped<IPointData, PointData>();
        services.AddScoped<IScheduleData, ScheduleData>();
        services.AddScoped<ISpecialDayData, SpecialDayData>();
        services.AddScoped<IUserData, UserData>();
        services.AddScoped<IValidityData, ValidityData>();

        return services;
    }
}