namespace SPW.Admin.Api.Features.User.DataAccess;

[ExcludeFromCodeCoverage]
internal sealed class UserData : IUserData
{
    private readonly IDynamoDBContext _dynamoDBContext;
    private readonly IAmazonDynamoDB _amazonDynamoDBClient;
    private readonly Table _table;

    public UserData(IDynamoDBContext dynamoDBContext, IAmazonDynamoDB amazonDynamoDBClient)
    {
        _dynamoDBContext = dynamoDBContext;
        _amazonDynamoDBClient = amazonDynamoDBClient;
        _table = Table.LoadTable(_amazonDynamoDBClient, UserEntity.TableName);
    }

    public async Task InsertAsync(UserEntity userEntity, CancellationToken cancellationToken)
    {
        var document = new Document
        {
            ["id"] = userEntity.Id,
            ["creation_date"] = userEntity.CreationDate,
            ["name"] = userEntity.Name,
            ["email"] = userEntity.Email,
            ["phonenumber"] = userEntity.PhoneNumber,
            ["gender"] = userEntity.Gender,
            ["birthdate"] = userEntity.BirthDate,
            ["baptismdate"] = userEntity.BaptismDate,
            ["privilege"] = userEntity.Privilege
        };

        await _table.PutItemAsync(document, cancellationToken);
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