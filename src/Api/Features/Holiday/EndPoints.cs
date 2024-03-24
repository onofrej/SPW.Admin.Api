using SPW.Admin.Api.Features.Holiday.Create;
using SPW.Admin.Api.Features.Holiday.Delete;
using SPW.Admin.Api.Features.Holiday.GetAll;
using SPW.Admin.Api.Features.Holiday.GetById;
using SPW.Admin.Api.Features.Holiday.Update;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Holiday;

[ExcludeFromCodeCoverage]
public sealed class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/holidays")
           .WithTags("Holidays");

        group.MapGet(string.Empty, GetHolidaysAsync);
        group.MapGet("/{id:guid}", GetByIdAsync);
        group.MapPost(string.Empty, CreateHolidayAsync);
        group.MapPut(string.Empty, UpdateHolidayAsync);
        group.MapDelete("/{id:guid}", DeleteHolidayAsync);
    }

    public static async Task<IResult> GetHolidaysAsync(ISender _sender, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllQuery(), cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Holidays retrieved with success - count: {count}", result.Data!.Count());

        return Results.Ok(new Response<IEnumerable<HolidayEntity>>(result.Data));
    }

    public static async Task<IResult> GetByIdAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var query = new GetByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Holiday by id retrieved with success: {input}", query);

        return Results.Ok(new Response<HolidayEntity>(result.Data));
    }

    public static async Task<IResult> CreateHolidayAsync(CreateRequest request,
        ISender _sender,
        CancellationToken cancellationToken)
    {
        var command = new CreateCommand
        {
            Name = request.Name!,
            Date = request.Date!
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Holiday created with success: {input}", command);

        return Results.Created($"/holidays/{result.Data}", new Response<Guid>(result.Data));
    }

    public static async Task<IResult> UpdateHolidayAsync(UpdateRequest request,
        ISender _sender,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCommand
        {
            Id = request.Id,
            Name = request.Name!,
            Date = request.Date!
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Holiday updated with success: {input}", command);

        return Results.Ok(new Response<Guid>(result.Data));
    }

    public static async Task<IResult> DeleteHolidayAsync([FromRoute] Guid id,
        ISender _sender,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCommand { Id = id };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Holiday deleted with success: {input}", command);

        return Results.NoContent();
    }
}