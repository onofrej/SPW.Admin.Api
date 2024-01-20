namespace SPW.Admin.Api.Features.Validity.DataAcces;

[ExcludeFromCodeCoverage]
internal class ValidityData : IValidityData
{
    private readonly IDynamoDBContext _dynamoDBContext;
    private readonly IAmazonDynamoDB _amazonDynamoDBClient;
    private readonly Table _table;

    public ValidityData(IDynamoDBContext dynamoDBContext, IAmazonDynamoDB amazonDynamoDBClient)
    {
        _dynamoDBContext = dynamoDBContext;
        _amazonDynamoDBClient = amazonDynamoDBClient;
        _table = Table.LoadTable(_amazonDynamoDBClient, ValidityEntity.TableName);
    }

    public async Task InsertAsync(ValidityEntity validityEntity, CancellationToken cancellationToken)
    {
        var document = new Document
        {
            ["id"] = validityEntity.Id,
            ["startdate"] = validityEntity.StartDate,
            ["enddate"] = validityEntity.EndDate,
            ["status"] = validityEntity.Status
        };

        await _table.PutItemAsync(document, cancellationToken);
    }

    public Task UpdateAsync(ValidityEntity validityEntity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(ValidityEntity validityEntity, CancellationToken cancellationToken)
    {
        return _dynamoDBContext.DeleteAsync(validityEntity, cancellationToken);
    }

    public async Task<ValidityEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dynamoDBContext.LoadAsync<ValidityEntity>(id, cancellationToken);
    }

    public async Task<IEnumerable<ValidityEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dynamoDBContext.ScanAsync<ValidityEntity>(default).GetRemainingAsync(cancellationToken);
    }
}