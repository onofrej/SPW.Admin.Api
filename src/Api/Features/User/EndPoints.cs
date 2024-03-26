using SPW.Admin.Api.Features.User.Create;
using SPW.Admin.Api.Features.User.Delete;
using SPW.Admin.Api.Features.User.GetAll;
using SPW.Admin.Api.Features.User.GetById;
using SPW.Admin.Api.Features.User.Update;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User;

[ExcludeFromCodeCoverage]
public sealed class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/users")
          .WithTags("Users");

        group.MapGet(string.Empty, GetUsersAsync);
        group.MapGet("/{id:guid}", GetByIdAsync);
        group.MapPost(string.Empty, CreateUserAsync);
        group.MapPut(string.Empty, UpdateUserAsync);
        group.MapDelete("/{id:guid}", DeleteUserAsync);
    }

    public static async Task<IResult> GetUsersAsync(ISender _sender, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAllQuery(), cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Users retreived with success - count: {count}", result.Data!.Count());

        return Results.Ok(new Response<IEnumerable<UserEntity>>(result.Data));
    }

    public static async Task<IResult> GetByIdAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var query = new GetByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("User by id retrieved with success: {input}", query);

        return Results.Ok(new Response<UserEntity>(result.Data));
    }

    public static async Task<IResult> CreateUserAsync(CreateRequest request,
        ISender _sender,
        CancellationToken cancellationToken)
    {
        var command = new CreateCommand
        {
            BaptismDate = request.BaptismDate!,
            BirthDate = request.BirthDate!,
            CongregationId = request.CongregationId!,
            Email = request.Email!,
            Gender = request.Gender!,
            Name = request.Name!,
            PhoneNumber = request.PhoneNumber!,
            Privilege = request.Privilege!
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("User created with success: {input}", command);

        return Results.Created($"/users/{result.Data}", new Response<Guid>(result.Data));
    }

    public static async Task<IResult> UpdateUserAsync(UpdateRequest request,
        ISender _sender,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCommand
        {
            BaptismDate = request.BaptismDate!,
            BirthDate = request.BirthDate!,
            CongregationId = request.CongregationId,
            Email = request.Email!,
            Gender = request.Gender!,
            Id = request.Id,
            Name = request.Name!,
            PhoneNumber = request.PhoneNumber!,
            Privilege = request.Privilege!
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("User updated with success: {input}", command);

        return Results.Ok(new Response<Guid>(result.Data));
    }

    public static async Task<IResult> DeleteUserAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new DeleteCommand { Id = id };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("User deleted with success: {input}", command);

        return Results.NoContent();
    }
}