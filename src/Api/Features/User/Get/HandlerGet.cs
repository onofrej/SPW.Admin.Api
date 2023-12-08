using SPW.Admin.Api.Features.User.DataAccess;

namespace SPW.Admin.Api.Features.User.Get;

[ExcludeFromCodeCoverage]
public sealed class HandlerGet
{
    private readonly IDynamoDBContext _dynamoDBContext;

    public HandlerGet(IDynamoDBContext dynamoDBContext)
    {
        _dynamoDBContext = dynamoDBContext;
    }

    public async Task<IResult> GetAllUsersAsync()
    {
        var user = await _dynamoDBContext.ScanAsync<UserEntity>(default).GetRemainingAsync();
        return Results.Ok(user);
    }

    public async Task<IResult> GetUserByIdAsync(Guid Id, string Name)
    {
        var user = await _dynamoDBContext.LoadAsync<UserEntity>(Id, Name);
        if (user is null)
        {
            return Results.NotFound("User not found");
        }
        return Results.Ok(user);
    }
}