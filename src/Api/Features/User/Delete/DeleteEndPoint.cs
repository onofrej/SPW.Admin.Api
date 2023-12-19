using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.Delete;

[ExcludeFromCodeCoverage]
public sealed class DeleteEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/users/{id:guid}", DeleteUserAsync);
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