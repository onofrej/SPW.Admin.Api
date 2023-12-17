namespace SPW.Admin.Api.Features.User.Delete;

[ExcludeFromCodeCoverage]
public sealed class EndPoint : ICarterModule
{
    private readonly HandlerDelete _query;

    public EndPoint(HandlerDelete query)
    {
        _query = query;
    }

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/users/{Id}", DeleteUserAsync);
    }

    public async Task<IResult> DeleteUserAsync(Guid Id, string Name)
    {
        return await _query.DeleteUserAsync(Id, Name);
    }
}