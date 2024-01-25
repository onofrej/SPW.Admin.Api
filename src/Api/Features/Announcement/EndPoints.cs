﻿using SPW.Admin.Api.Features.Announcement.Create;
using SPW.Admin.Api.Features.Announcement.DataAccess;
using SPW.Admin.Api.Features.Announcement.Delete;
using SPW.Admin.Api.Features.Announcement.GetAll;
using SPW.Admin.Api.Features.Announcement.GetById;
using SPW.Admin.Api.Features.Announcement.Update;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Announcement;

public sealed class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/announcements")
            .WithTags("Announcements");

        group.MapGet(string.Empty, GetAnnouncementsAsync);
        group.MapGet("/{id:guid}", GetByIdAsync);
        group.MapPost(string.Empty, CreateAnnouncementAsync);
        group.MapPut(string.Empty, UpdateAnnouncementsync);
        group.MapDelete("/{id:guid}", DeleteAnnouncementAsync);
    }

    public static async Task<IResult> GetAnnouncementsAsync(ISender _sender, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllQuery(), cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Announcements retreived with success - count: {count}", result.Data!.Count());

        return Results.Ok(new Response<IEnumerable<AnnouncementEntity>>(result.Data));
    }

    public static async Task<IResult> GetByIdAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var query = new GetByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Announcement by id retrieved with success: {input}", query);

        return Results.Ok(new Response<AnnouncementEntity>(result.Data));
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

    public static async Task<IResult> UpdateAnnouncementsync(UpdateRequest request,
       ISender _sender,
       CancellationToken cancellationToken)
    {
        var command = new UpdateCommand
        {
            Id = request.Id,
            Title = request.Title,
            Message = request.Message,
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Announcement updated with success: {input}", command);

        return Results.Ok(new Response<Guid>(result.Data));
    }

    public static async Task<IResult> DeleteAnnouncementAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new DeleteCommand { Id = id };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Announcement deleted with success: {input}", command);

        return Results.NoContent();
    }
}