﻿using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/users", CreateUserAsync);
    }

    public static async Task<IResult> CreateUserAsync(CreateRequest request,
        ISender _sender,
        CancellationToken cancellationToken)
    {
        var command = new CreateCommand
        {
            Name = request.Name!,
            Email = request.Email!,
            PhoneNumber = request.PhoneNumber!,
            Gender = request.Gender!,
            BirthDate = request.BirthDate!,
            BaptismDate = request.BaptismDate!,
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
}