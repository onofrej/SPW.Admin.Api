using SPW.Admin.Api.Features.User.DataAccess;

namespace SPW.Admin.Api.Features.User.Put;

[ExcludeFromCodeCoverage]
public sealed class EndPoint : ICarterModule
{
    private readonly HandlerPut _query;

    public EndPoint(HandlerPut query)
    {
        _query = query;
    }

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/users", PutUserAsync);
    }

    internal async Task<IResult> PutUserAsync(UserEntity userRequest)
    {
        return await _query.PutUserAsync(userRequest);
    }
}