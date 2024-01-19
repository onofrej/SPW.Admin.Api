using SPW.Admin.Api.Features.Validity.Create;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Validity;

[ExcludeFromCodeCoverage]
public sealed class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/validities", CreateValidityAsync);
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