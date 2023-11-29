using Serilog;

namespace SPW.Admin.Api.Users.UseCases.Create;

internal static class Endpoints
{
    public static void MapEndpoints(this WebApplication webApplication)
    {
        webApplication.MapPost("/users", CreateUserAsync);
    }

    public static async Task<IResult> CreateUserAsync(Request request,
        IUseCase _createUserUseCase,
        CancellationToken cancellationToken)
    {
        var input = new Input(Guid.NewGuid().ToString(),
            request.Name!);

        await _createUserUseCase.ExecuteAsync(input, cancellationToken);

        Log.Information("User created with success: {input}", input);

        return Results.Created($"/users/{input.Id}", input);
    }
}