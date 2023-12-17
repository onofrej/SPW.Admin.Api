namespace SPW.Admin.Api.Features.User.Get;

[ExcludeFromCodeCoverage]
public sealed class EndPoint : ICarterModule
{
    private readonly HandlerGet _query;

    public EndPoint(HandlerGet query)
    {
        _query = query;
    }

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/users", GetAllUsersAsync);
        app.MapGet("/users/{Id}", GetUserByIdAsync);
    }

    public async Task<IResult> GetAllUsersAsync()
    {
        return await _query.GetAllUsersAsync();
    }

    public async Task<IResult> GetUserByIdAsync(Guid Id, string Name)
    {
        return await _query.GetUserByIdAsync(Id, Name);
    }
}