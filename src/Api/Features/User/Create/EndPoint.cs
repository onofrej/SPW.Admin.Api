﻿using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.Create;

[ExcludeFromCodeCoverage]
public sealed class EndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/users", CreateUserAsync)
            .WithName("users")
            .WithOpenApi();
    }

    public static async Task<IResult> CreateUserAsync(Request request,
        ISender _sender,
        CancellationToken cancellationToken)
    {
        var command = new Command { Name = request.Name! };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("User created with success: {input}", command);

        return Results.Created($"/users/{result.Data}", new Response<Guid>(result.Data, default));
    }
}