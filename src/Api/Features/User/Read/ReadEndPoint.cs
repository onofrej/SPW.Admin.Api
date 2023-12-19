using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.Read
{
    public class ReadEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/users/{id:guid}", GetByIdAsync);
        }

        public static async Task<IResult> GetByIdAsync([FromRoute] Guid id, ISender _sender, CancellationToken cancellationToken)
        {
            var command = new ReadCommand
            {
                Id = id,
            };

            var result = await _sender.Send(command, cancellationToken);

            if (result.HasFailed)
            {
                return Results.BadRequest(new Response<Guid>(Guid.Empty, result.Error));
            }

            Log.Information("User by id read with success: {input}", command);

            var userData = new ReadCommand
            {
                Id = command.Id,
                Name = command.Name,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                Gender = command.Gender,
                BirthDate = command.BirthDate,
                BaptismDate = command.BirthDate,
                Privilege = command.Privilege,
            };

            return Results.Ok(userData);
        }
    }
}