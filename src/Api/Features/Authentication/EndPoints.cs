﻿using Microsoft.AspNetCore.Authorization;
using SPW.Admin.Api.Features.Authentication.Login;
using SPW.Admin.Api.Features.Authentication.Register;
using SPW.Admin.Api.Features.Authentication.Services;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Authentication;

public class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/authentication")
          .WithTags("Authentication");

        group.MapPost("/register", RegisterAuthenticationAsync);
        group.MapPost("/login", LoginAuthenticationAsync);
    }

    [AllowAnonymous]
    public static async Task<IResult> RegisterAuthenticationAsync(RegisterRequest request, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new RegisterCommand
        {
            Email = request.Email!,
            Password = request.Password!,
            ConfirmPassword = request.ConfirmPassword!
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("User authentication created with success: {input}", command);

        return Results.Created($"/authentication/{result.Data}", new Response<Guid>(result.Data));
    }

    [AllowAnonymous]
    public static async Task<IResult> LoginAuthenticationAsync([FromBody] LoginQuery request, ISender _sender, CancellationToken cancellationToken)
    {
        TokenService generateToken = new();

        var query = new LoginQuery(request.Email, request.Password);

        var result = await _sender.Send(query, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result?.Error));
        }

        var authenticationEntity = result.Data;

        var authToken = generateToken.GenerateToken(authenticationEntity!);

        return Results.Ok(new Response<Guid>(result.Data!.Id, authToken));

        //Erro:
        //"Data": "",
        //"Errors": "Unable to cast object of type 'System.String' to type 'System.Guid'."
    }
}