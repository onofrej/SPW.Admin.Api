using SPW.Admin.Api.Features.Validity.Create;
using SPW.Admin.Api.Features.Validity.Delete;
using SPW.Admin.Api.Features.Validity.GetAll;
using SPW.Admin.Api.Features.Validity.GetById;
using SPW.Admin.Api.Features.Validity.Update;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Validity;

public sealed class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/validities")
            .WithTags("Validities");

        group.MapGet(string.Empty, GetValiditiesAsync);
        group.MapGet("/{id:guid}", GetByIdAsync);
        group.MapPost(string.Empty, CreateValidityAsync);
        group.MapPut(string.Empty, UpdateValidityAsync);
        group.MapDelete("/{id:guid}", DeleteValidityAsync);
    }

    public static async Task<IResult> GetValiditiesAsync(ISender _sender, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllQuery(), cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Validities retreived with success - count: {count}", result.Data!.Count());

        return Results.Ok(new Response<IEnumerable<ValidityEntity>>(result.Data));
    }

    public static async Task<IResult> GetByIdAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var query = new GetByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Validity by id retrieved with success: {input}", query);

        return Results.Ok(new Response<ValidityEntity>(result.Data));
    }

    public static async Task<IResult> CreateValidityAsync(CreateRequest request,
    ISender _sender,
    CancellationToken cancellationToken)
    {
        var command = new CreateCommand
        {
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = request.Status
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Validity created with success: {input}", command);

        return Results.Created($"/validities/{result.Data}", new Response<Guid>(result.Data));
    }

    public static async Task<IResult> UpdateValidityAsync(UpdateRequest request,
        ISender _sender,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCommand
        {
            Id = request.Id,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = request.Status
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Validity updated with success: {input}", command);

        return Results.Ok(new Response<Guid>(result.Data));
    }

    public static async Task<IResult> DeleteValidityAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new DeleteCommand { Id = id };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Validity deleted with success: {input}", command);

        return Results.NoContent();
    }
}