using SPW.Admin.Api.Features.SpecialDay.Delete;
using SPW.Admin.Api.Features.SpecialDay.Create;
using SPW.Admin.Api.Features.SpecialDay.GetAll;
using SPW.Admin.Api.Features.SpecialDay.DataAccess;
using SPW.Admin.Api.Features.SpecialDay.GetById;
using SPW.Admin.Api.Features.SpecialDay.Update;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDate;

[ExcludeFromCodeCoverage]
public class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/specialdays")
         .WithTags("Special Days");

        group.MapGet(string.Empty, GetSpecialDaysAsync);
        group.MapGet("/{id:guid}", GetByIdAsync);
        group.MapPost(string.Empty, CreateSpecialDayAsync);
        group.MapPut(string.Empty, UpdateSpecialDayAsync);
        group.MapDelete("/{id:guid}", DeleteSpecialDayAsync);
    }

    public static async Task<IResult> GetSpecialDaysAsync(ISender _sender, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllQuery(), cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Special days retreived with success - count: {count}", result.Data!.Count());

        return Results.Ok(new Response<IEnumerable<SpecialDayEntity>>(result.Data));
    }

    public static async Task<IResult> GetByIdAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var query = new GetByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Special day by id retrieved with success: {input}", query);

        return Results.Ok(new Response<SpecialDayEntity>(result.Data));
    }

    public static async Task<IResult> CreateSpecialDayAsync(CreateRequest request,
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

        Log.Information("Special Day created with success: {input}", command);

        return Results.Created($"/specialdays/{result.Data}", new Response<Guid>(result.Data));
    }

    public static async Task<IResult> UpdateSpecialDayAsync(UpdateRequest request,
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

        Log.Information("Special day updated with success: {input}", command);

        return Results.Ok(new Response<Guid>(result.Data));
    }

    public static async Task<IResult> DeleteSpecialDayAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new DeleteCommand { Id = id };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Special day deleted with success: {input}", command);

        return Results.NoContent();
    }
}