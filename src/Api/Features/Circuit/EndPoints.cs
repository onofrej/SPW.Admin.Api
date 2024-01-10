using SPW.Admin.Api.Features.Circuit.Create;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Circuit;

public sealed class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/circuits", CreateCircuitsAsync);
    }

    public static async Task<IResult> CreateCircuitsAsync(CreateRequest request, ISender _sender, CancellationToken cancellationToken)
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

        Log.Information("Circuit created with success: {input}", command);

        return Results.Created($"/circuits/{result.Data}", new Response<Guid>(result.Data));
    }
}