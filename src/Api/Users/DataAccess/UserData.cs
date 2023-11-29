namespace SPW.Admin.Api.Users.DataAccess;

internal sealed class UserData : IUserData
{
    private readonly IDynamoDBContext _dynamoDBContext;

    public UserData(IDynamoDBContext dynamoDBContext)
    {
        _dynamoDBContext = dynamoDBContext;
    }

    public Task InsertAsync(UserEntity userEntity, CancellationToken cancellationToken)
    {
        return _dynamoDBContext.SaveAsync(userEntity, cancellationToken);
    }
}