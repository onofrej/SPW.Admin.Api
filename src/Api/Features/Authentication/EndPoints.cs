namespace SPW.Admin.Api.Features.Authentication;

public class EndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/authentication")
          .WithTags("Authentication");

        group.MapPost("/register", CreateAuthenticationAsync);
        group.MapPost("/login", LoginAuthenticationAsync);
    }

    public static async Task<IResult> CreateAuthenticationAsync()
    {
        return Results.Ok();
    }

    public static async Task<IResult> LoginAuthenticationAsync()
    {
        return Results.Ok();
    }
}