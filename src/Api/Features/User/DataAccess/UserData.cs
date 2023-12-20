namespace SPW.Admin.Api.Features.User.DataAccess;

[ExcludeFromCodeCoverage]
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

    public async Task<IEnumerable<UserEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dynamoDBContext.ScanAsync<UserEntity>(default).GetRemainingAsync(cancellationToken);
    }
}