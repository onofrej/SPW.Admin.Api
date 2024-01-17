using SPW.Admin.Api.Features.Point.Create;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Point;

public sealed class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/points", CreatePointAsync);
    }

    public static async Task<IResult> CreatePointAsync(CreateRequest request, ISender _sender, CancellationToken cancellationToken)
    {
        var command = new CreateCommand
        {
            Name = request.Name,
            QuantityPublishers = request.QuantityPublishers,
            Address = request.Address,
            ImageUrl = request.ImageUrl,
            GoogleMapsUrl = request.GoogleMapsUrl,
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.HasFailed)
        {
            return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
        }

        Log.Information("Point created with success: {input}", command);

        return Results.Created($"/points/{result.Data}", new Response<Guid>(result.Data));
    }
}