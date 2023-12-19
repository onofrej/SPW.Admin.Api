namespace SPW.Admin.Api.Features.User.DataAccess;

[ExcludeFromCodeCoverage]
internal sealed class UserData : IUserData
{
    private readonly IDynamoDBContext _dynamoDBContext;
    private readonly IAmazonDynamoDB _dynamoDbClient;

    public UserData(IDynamoDBContext dynamoDBContext, IAmazonDynamoDB dynamoDbClient)
    {
        _dynamoDBContext = dynamoDBContext;
        _dynamoDbClient = dynamoDbClient;
    }

    public Task InsertAsync(UserEntity userEntity, CancellationToken cancellationToken)
    {
        return _dynamoDBContext.SaveAsync(userEntity, cancellationToken);
    }

    public Task UpdateAsync(UserEntity userEntity, CancellationToken cancellationToken)
    {
        return _dynamoDBContext.SaveAsync(userEntity, cancellationToken);
    }

    public Task DeleteAsync(UserEntity userEntity, CancellationToken cancellationToken)
    {
        return _dynamoDBContext.DeleteAsync(userEntity, cancellationToken);
    }

    public async Task<UserEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dynamoDBContext.LoadAsync<UserEntity>(id, cancellationToken);
    }
}