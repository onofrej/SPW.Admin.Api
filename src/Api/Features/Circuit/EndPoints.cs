using SPW.Admin.Api.Features.Circuit.Create;
using SPW.Admin.Api.Features.Circuit.DataAccess;
using SPW.Admin.Api.Features.Circuit.Delete;
using SPW.Admin.Api.Features.Circuit.GetAll;
using SPW.Admin.Api.Features.Circuit.GetById;
using SPW.Admin.Api.Features.Circuit.Update;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Circuit;

public sealed class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/circuits", GetCircuitsAsync);
        app.MapGet("/circuits/{id:guid}", GetByIdAsync);
        app.MapPost("/circuits", CreateCircuitsAsync);
        app.MapPut("/circuits", UpdateCircuitAsync);
        app.MapDelete("/circuits/{id:guid}", DeleteCircuitAsync);
    }

    public static async Task<IResult> GetCircuitsAsync(ISender _sender, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllQuery(), cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result?.Error));
        }

        Log.Information("Circuits retreived with success - count: {count}", result.Data!.Count());

        return Results.Ok(new Response<IEnumerable<CircuitEntity>>(result.Data));
    }

    public static async Task<IResult> GetByIdAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var query = new GetByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Circuit by id retrieved with success: {input}", query);

        return Results.Ok(new Response<CircuitEntity>(result.Data));
    }

    public static async Task<IResult> CreateCircuitsAsync(CreateRequest request, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new CreateCommand
        {
            Name = request.Name!
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Circuit created with success: {input}", command);

        return Results.Created($"/circuits/{result.Data}", new Response<Guid>(result.Data));
    }

    public static async Task<IResult> UpdateCircuitAsync(UpdateRequest request, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new UpdateCommand
        {
            Id = request.Id,
            Name = request.Name,
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Circuit updated with success: {input}", command);

        return Results.Ok(new Response<Guid>(result.Data));
    }

    public static async Task<IResult> DeleteCircuitAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new DeleteCommand { Id = id };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Circuit deleted with success: {input}", command);

        return Results.NoContent();
    }
}