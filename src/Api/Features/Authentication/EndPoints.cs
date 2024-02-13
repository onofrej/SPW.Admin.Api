using SPW.Admin.Api.Features.Authentication.Register;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Authentication;

public class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/authentication")
          .WithTags("Authentication");

        group.MapPost("/register", RegisterAuthenticationAsync);
    }

    public static async Task<IResult> RegisterAuthenticationAsync(AuthenticationRequest request, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new AuthenticationCommand
        {
            Name = request.Name!,
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
}