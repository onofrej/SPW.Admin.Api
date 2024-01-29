using SPW.Admin.Api.Features.SpecialDate.Delete;
using SPW.Admin.Api.Features.SpecialDate.Create;
using SPW.Admin.Api.Features.SpecialDate.GetAll;
using SPW.Admin.Api.Features.SpecialDate.DataAccess;
using SPW.Admin.Api.Shared.Models;
using SPW.Admin.Api.Features.SpecialDate.GetById;
using SPW.Admin.Api.Features.SpecialDate.Update;

namespace SPW.Admin.Api.Features.SpecialDate;

[ExcludeFromCodeCoverage]
public class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/specialdates")
         .WithTags("Special Dates");

        group.MapGet(string.Empty, GetSpecialDatesAsync);
        group.MapGet("/{id:guid}", GetByIdAsync);
        group.MapPost(string.Empty, CreateSpecialDateAsync);
        group.MapPut(string.Empty, UpdateSpecialDateAsync);
        group.MapDelete("/{id:guid}", DeleteSpecialDateAsync);
    }

    public static async Task<IResult> GetSpecialDatesAsync(ISender _sender, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllQuery(), cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Special dates retreived with success - count: {count}", result.Data!.Count());

        return Results.Ok(new Response<IEnumerable<SpecialDateEntity>>(result.Data));
    }

    public static async Task<IResult> GetByIdAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var query = new GetByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Special date by id retrieved with success: {input}", query);

        return Results.Ok(new Response<SpecialDateEntity>(result.Data));
    }

    public static async Task<IResult> CreateSpecialDateAsync(CreateRequest request,
       ISender _sender,
       CancellationToken cancellationToken)
    {
        var command = new CreateCommand
        {
            Name = request.Name!,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CircuitId = request.CircuitId,
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Special Date created with success: {input}", command);

        return Results.Created($"/specialdates/{result.Data}", new Response<Guid>(result.Data));
    }

    public static async Task<IResult> UpdateSpecialDateAsync(UpdateRequest request,
       ISender _sender,
       CancellationToken cancellationToken)
    {
        var command = new UpdateCommand
        {
            Id = request.Id,
            Name = request.Name!,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CircuitId = request.CircuitId,
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Special date updated with success: {input}", command);

        return Results.Ok(new Response<Guid>(result.Data));
    }

    public static async Task<IResult> DeleteSpecialDateAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new DeleteCommand { Id = id };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Special date deleted with success: {input}", command);

        return Results.NoContent();
    }
}