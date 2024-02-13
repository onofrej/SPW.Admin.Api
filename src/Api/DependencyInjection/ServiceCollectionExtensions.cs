using SPW.Admin.Api.Features.Announcement.DataAccess;
using SPW.Admin.Api.Features.Authentication.DataAccess;
using SPW.Admin.Api.Features.Circuit.DataAccess;
using SPW.Admin.Api.Features.Holiday.DataAccess;
using SPW.Admin.Api.Features.Point.DataAccess;
using SPW.Admin.Api.Features.Schedule.DataAccess;
using SPW.Admin.Api.Features.SpecialDay.DataAccess;
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
        services.AddScoped<IValidityData, ValidityData>();
        services.AddScoped<IScheduleData, ScheduleData>();
        services.AddScoped<IHolidayData, HolidayData>();
        services.AddScoped<IAnnouncementData, AnnouncementData>();
        services.AddScoped<ISpecialDayData, SpecialDayData>();
        services.AddScoped<IAuthenticationData, AuthenticationData>();

        return services;
    }
}