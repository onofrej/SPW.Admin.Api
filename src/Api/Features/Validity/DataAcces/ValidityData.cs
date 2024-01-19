namespace SPW.Admin.Api.Features.Validity.DataAcces;

[ExcludeFromCodeCoverage]
internal class ValidityData : IValidtyData
{
    private readonly IDynamoDBContext _dynamoDBContext;
    private readonly IAmazonDynamoDB _amazonDynamoDBClient;
    private readonly Table _table;

    public ValidityData(IDynamoDBContext dynamoDBContext, IAmazonDynamoDB amazonDynamoDBClient, Table table)
    {
        _dynamoDBContext = dynamoDBContext;
        _amazonDynamoDBClient = amazonDynamoDBClient;
        _table = table;
    }

    public Task InsertAsync(ValidityEntity validityEntity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(ValidityEntity validityEntity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(ValidityEntity validityEntity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ValidityEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ValidityEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}