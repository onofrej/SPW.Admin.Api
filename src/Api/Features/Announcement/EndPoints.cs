using SPW.Admin.Api.Features.Announcement.Create;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Announcement;

public sealed class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/announcements")
            .WithTags("Announcements");

        //group.MapGet(string.Empty, GetAnnouncementsAsync);
        //group.MapGet("/{id:guid}", GetByIdAsync);
        group.MapPost(string.Empty, CreateAnnouncementAsync);
        //group.MapPut(string.Empty, UpdateAnnouncementsync);
        //group.MapDelete("/{id:guid}", DeleteAnnouncementAsync);
    }

    public static async Task<IResult> CreateAnnouncementAsync(CreateRequest request, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new CreateCommand
        {
            Title = request.Title,
            Message = request.Message,
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Announcement created with success: {input}", command);

        return Results.Created($"/announcements/{result.Data}", new Response<Guid>(result.Data));
    }
}