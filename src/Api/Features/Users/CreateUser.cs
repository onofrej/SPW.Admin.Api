using SPW.Admin.Api.Features.Users.DataAccess;

namespace SPW.Admin.Api.Features.Users;

public static class CreateUser
{
    public sealed class Request
    {
        public string? Name { get; set; }
    }

    internal sealed class EndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/users", CreateUserAsync);
        }

        public static async Task<IResult> CreateUserAsync(Request request,
            ISender _sender,
            CancellationToken cancellationToken)
        {
            var command = new Command(request.Name!);

            var result = await _sender.Send(command, cancellationToken);

            Log.Information("User created with success: {input}", command);

            return Results.Created($"/users/{result}", command);
        }
    }

    internal sealed record Command(string Name) : IRequest<Guid>;

    internal sealed class Handler : IRequestHandler<Command, Guid>
    {
        private readonly IUserData _userData;

        public Handler(IUserData userData)
        {
            _userData = userData;
        }

        public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity = new UserEntity
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreationDate = DateTime.UtcNow
            };

            await _userData.InsertAsync(entity, cancellationToken);

            return entity.Id;
        }
    }
}