using SPW.Admin.Api.Features.Validity.Create;
using SPW.Admin.Api.Features.Validity.DataAcces;
using SPW.Admin.Api.Features.Validity.GetAll;
using SPW.Admin.Api.Features.Validity.GetById;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Validity;

[ExcludeFromCodeCoverage]
public sealed class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/validities", GetValiditiesAsync);
        app.MapGet("/validities/{id:guid}", GetByIdAsync);
        app.MapPost("/validities", CreateValidityAsync);
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
            Status = request.Status,
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Validity created with success: {input}", command);

        return Results.Created($"/validities/{result.Data}", new Response<Guid>(result.Data));
    }
}