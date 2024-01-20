using SPW.Admin.Api.Features.Schedule.Create;
using SPW.Admin.Api.Features.Schedule.DataAccess;
using SPW.Admin.Api.Features.Schedule.Delete;
using SPW.Admin.Api.Features.Schedule.GetAll;
using SPW.Admin.Api.Features.Schedule.GetById;
using SPW.Admin.Api.Features.Schedule.Update;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Schedule;

[ExcludeFromCodeCoverage]
public sealed class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/schedules")
           .WithTags("Schedules");

        group.MapGet(string.Empty, GetSchedulesAsync);
        group.MapGet("/{id:guid}", GetByIdAsync);
        group.MapPost(string.Empty, CreateScheduleAsync);
        group.MapPut(string.Empty, UpdateScheduleAsync);
        group.MapDelete("/{id:guid}", DeleteScheduleAsync);
    }

    public static async Task<IResult> GetSchedulesAsync(ISender _sender, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllQuery(), cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Schedules retrieved with success - count: {count}", result.Data!.Count());

        return Results.Ok(new Response<IEnumerable<ScheduleEntity>>(result.Data));
    }

    public static async Task<IResult> GetByIdAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var query = new GetByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Schedule by id retrieved with success: {input}", query);

        return Results.Ok(new Response<ScheduleEntity>(result.Data));
    }

    public static async Task<IResult> CreateScheduleAsync(CreateRequest request,
        ISender _sender,
        CancellationToken cancellationToken)
    {
        var command = new CreateCommand
        {
            Time = request.Time!
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Schedule created with success: {input}", command);

        return Results.Created($"/schedules/{result.Data}", new Response<Guid>(result.Data));
    }

    public static async Task<IResult> UpdateScheduleAsync(UpdateRequest request,
        ISender _sender,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCommand
        {
            Id = request.Id,
            Time = request.Time!
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Schedule updated with success: {input}", command);

        return Results.Ok(new Response<Guid>(result.Data));
    }

    public static async Task<IResult> DeleteScheduleAsync([FromRoute] Guid id,
        ISender _sender,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCommand { Id = id };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Schedule deleted with success: {input}", command);

        return Results.NoContent();
    }
}