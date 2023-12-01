namespace SPW.Admin.Api.Features.User;

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