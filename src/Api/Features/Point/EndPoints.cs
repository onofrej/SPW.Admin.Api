using SPW.Admin.Api.Features.Point.Create;
using SPW.Admin.Api.Features.Point.DataAccess;
using SPW.Admin.Api.Features.Point.Delete;
using SPW.Admin.Api.Features.Point.GetAll;
using SPW.Admin.Api.Features.Point.GetById;
using SPW.Admin.Api.Features.Point.Update;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Point;

public sealed class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/points", GetPointsAsync);
        app.MapGet("/points/{id:guid}", GetByIdAsync);
        app.MapPost("/points", CreatePointAsync);
        app.MapPut("/points", UpdatePointAsync);
        app.MapDelete("/points/{id:guid}", DeletePointAsync);
    }

    public static async Task<IResult> GetPointsAsync(ISender _sender, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllQuery(), cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Points retreived with success - count: {count}", result.Data!.Count());

        return Results.Ok(new Response<IEnumerable<PointEntity>>(result.Data));
    }

    public static async Task<IResult> GetByIdAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var query = new GetByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Point by id retrieved with success: {input}", query);

        return Results.Ok(new Response<PointEntity>(result.Data));
    }

    public static async Task<IResult> CreatePointAsync(CreateRequest request, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new CreateCommand
        {
            Name = request.Name!,
            QuantityPublishers = request.QuantityPublishers,
            Address = request.Address!,
            ImageUrl = request.ImageUrl!,
            GoogleMapsUrl = request.GoogleMapsUrl!,
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Point created with success: {input}", command);

        return Results.Created($"/points/{result.Data}", new Response<Guid>(result.Data));
    }

    public static async Task<IResult> UpdatePointAsync(UpdateRequest request, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new UpdateCommand
        {
            Id = request.Id,
            Name = request.Name!,
            QuantityPublishers = request.QuantityPublishers,
            Address = request.Address!,
            ImageUrl = request.ImageUrl!,
            GoogleMapsUrl = request.GoogleMapsUrl!,
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Point updated with success: {input}", command);

        return Results.Ok(new Response<Guid>(result.Data));
    }

    public static async Task<IResult> DeletePointAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new DeleteCommand { Id = id };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Point deleted with success: {input}", command);

        return Results.NoContent();
    }
}