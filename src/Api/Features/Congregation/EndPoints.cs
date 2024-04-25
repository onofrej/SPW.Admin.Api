using SPW.Admin.Api.Features.Congregation.Create;
using SPW.Admin.Api.Features.Congregation.Delete;
using SPW.Admin.Api.Features.Congregation.GetAll;
using SPW.Admin.Api.Features.Congregation.GetById;
using SPW.Admin.Api.Features.Congregation.Update;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Congregation;

public sealed class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/congregations")
            .WithTags("Congregations");

        group.MapGet(string.Empty, GetCongregationsAsync);
        group.MapGet("/{id:guid}", GetByIdAsync);
        group.MapPost(string.Empty, CreateCongregationAsync);
        group.MapPut(string.Empty, UpdateCongregationAsync);
        group.MapDelete("/{id:guid}", DeleteCongregationAsync);
    }

    public static async Task<IResult> GetCongregationsAsync(ISender _sender, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllQuery(), cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result?.Error));
        }

        Log.Information("Congregations retreived with success - count: {count}", result.Data!.Count());

        return Results.Ok(new Response<IEnumerable<CongregationEntity>>(result.Data));
    }

    public static async Task<IResult> GetByIdAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var query = new GetByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Congregation by id retrieved with success: {input}", query);

        return Results.Ok(new Response<CongregationEntity>(result.Data));
    }

    public static async Task<IResult> CreateCongregationAsync(CreateRequest request, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new CreateCommand
        {
            Name = request.Name!,
            Number = request.Number!,
            CircuitId = request.CircuitId
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Congregation created with success: {input}", command);

        return Results.Created($"/congregations/{result.Data}", new Response<Guid>(result.Data));
    }

    public static async Task<IResult> UpdateCongregationAsync(UpdateRequest request, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new UpdateCommand
        {
            Id = request.Id,
            Name = request.Name!,
            Number = request.Number!,
            CircuitId = request.CircuitId
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Congregation updated with success: {input}", command);

        return Results.Ok(new Response<Guid>(result.Data));
    }

    public static async Task<IResult> DeleteCongregationAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new DeleteCommand { Id = id };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Congregation deleted with success: {input}", command);

        return Results.NoContent();
    }
}