using SPW.Admin.Api.Features.Authentication.Create;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Authentication;

public class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/authentication")
          .WithTags("Authentication");

        group.MapPost(string.Empty, CreateAuthenticationAsync);
        group.MapPost("/login", LoginAuthenticationAsync);
    }

    public static async Task<IResult> CreateAuthenticationAsync(CreateRequest request, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new CreateCommand
        {
            UserName = request.UserName,
            Password = request.Password
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Authentication created with success: {input}", command);

        return Results.Created($"/authentication/{result.Data}", new Response<Guid>(result.Data));
    }

    public static async Task<IResult> LoginAuthenticationAsync()
    {
        return Results.Ok();
    }
}