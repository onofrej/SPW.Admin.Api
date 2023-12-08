using SPW.Admin.Api.Features.User.DataAccess;

namespace SPW.Admin.Api.Features.User.Delete;

[ExcludeFromCodeCoverage]
public sealed class HandlerDelete
{
    private readonly IDynamoDBContext _dynamoDBContext;

    public HandlerDelete(IDynamoDBContext dynamoDBContext)
    {
        _dynamoDBContext = dynamoDBContext;
    }

    public async Task<IResult> DeleteUserAsync(Guid Id, string Name)
    {
        var user = await _dynamoDBContext.LoadAsync<UserEntity>(Id, Name);
        if (user is null)
        {
            return Results.NotFound("User not found");
        }
        await _dynamoDBContext.DeleteAsync(user);
        return Results.Ok("User deleted with success");
    }
}