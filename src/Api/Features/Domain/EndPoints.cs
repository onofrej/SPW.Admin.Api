using SPW.Admin.Api.Features.Domain.Create;
using SPW.Admin.Api.Features.Domain.Delete;
using SPW.Admin.Api.Features.Domain.GetAll;
using SPW.Admin.Api.Features.Domain.GetById;
using SPW.Admin.Api.Features.Domain.Update;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Domain;

[ExcludeFromCodeCoverage]
public sealed class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/domains")
          .WithTags("Domains");

        group.MapGet(string.Empty, GetDomainsAsync);
        group.MapGet("/{id:guid}", GetByIdAsync);
        group.MapPost(string.Empty, CreateDomainAsync);
        group.MapPut(string.Empty, UpdateDomainAsync);
        group.MapDelete("/{id:guid}", DeleteDomainAsync);
    }

    public static async Task<IResult> GetDomainsAsync(ISender _sender, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllQuery(), cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Domains retreived with success - count: {count}", result.Data!.Count());

        return Results.Ok(new Response<IEnumerable<DomainEntity>>(result.Data));
    }

    public static async Task<IResult> GetByIdAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var query = new GetByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Domain by id retrieved with success: {input}", query);

        return Results.Ok(new Response<DomainEntity>(result.Data));
    }

    public static async Task<IResult> CreateDomainAsync(CreateRequest request,
        ISender _sender,
        CancellationToken cancellationToken)
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

        Log.Information("Domain created with success: {input}", command);

        return Results.Created($"/domains/{result.Data}", new Response<Guid>(result.Data));
    }

    public static async Task<IResult> UpdateDomainAsync(UpdateRequest request,
        ISender _sender,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCommand
        {
            Id = request.Id,
            Name = request.Name!
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Domain updated with success: {input}", command);

        return Results.Ok(new Response<Guid>(result.Data));
    }

    public static async Task<IResult> DeleteDomainAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new DeleteCommand { Id = id };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Domain deleted with success: {input}", command);

        return Results.NoContent();
    }
}