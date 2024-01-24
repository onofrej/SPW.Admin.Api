namespace SPW.Admin.Api.Features.Announcement;

[ExcludeFromCodeCoverage]
public sealed class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/announcements")
            .WithTags("Announcements");

        //group.MapGet(string.Empty, GetAnnouncementsAsync);
        //group.MapGet("/{id:guid}", GetByIdAsync);
        //group.MapPost(string.Empty, CreateAnnouncementAsync);
        //group.MapPut(string.Empty, UpdateAnnouncementsync);
        //group.MapDelete("/{id:guid}", DeleteAnnouncementAsync);
    }
}