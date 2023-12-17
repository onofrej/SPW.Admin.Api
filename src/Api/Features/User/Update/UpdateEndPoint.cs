using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/users", UpdateUserAsync);
    }

    public static async Task<IResult> UpdateUserAsync(UpdateRequest request,
        ISender _sender,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCommand
        {
            Id = request.Id,
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

        Log.Information("User updated with success: {input}", command);

        return Results.Ok(new Response<Guid>(result.Data));
    }
}