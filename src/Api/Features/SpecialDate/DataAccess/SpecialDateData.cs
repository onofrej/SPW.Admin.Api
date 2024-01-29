namespace SPW.Admin.Api.Features.SpecialDate.DataAccess;

[ExcludeFromCodeCoverage]
internal sealed class SpecialDateData : ISpecialDateData
{
    private readonly IDynamoDBContext _dynamoDBContext;
    private readonly IAmazonDynamoDB _amazonDynamoDBClient;
    private readonly Table _table;

    public SpecialDateData(IDynamoDBContext dynamoDBContext, IAmazonDynamoDB amazonDynamoDBClient)
    {
        _dynamoDBContext = dynamoDBContext;
        _amazonDynamoDBClient = amazonDynamoDBClient;
        _table = Table.LoadTable(_amazonDynamoDBClient, SpecialDateEntity.TableName);
    }

    public async Task InsertAsync(SpecialDateEntity specialDateEntity, CancellationToken cancellationToken)
    {
        var document = new Document
        {
            ["id"] = specialDateEntity.Id,
            ["name"] = specialDateEntity.Name,
            ["startdate"] = specialDateEntity.StartDate,
            ["enddate"] = specialDateEntity.EndDate,
            ["circuitId"] = specialDateEntity.CircuitId
        };

        await _table.PutItemAsync(document, cancellationToken);
    }

    public Task UpdateAsync(SpecialDateEntity specialDateEntity, CancellationToken cancellationToken)
    {
        return _dynamoDBContext.SaveAsync(specialDateEntity, cancellationToken);
    }

    public Task DeleteAsync(SpecialDateEntity specialDateEntity, CancellationToken cancellationToken)
    {
        return _dynamoDBContext.DeleteAsync(specialDateEntity, cancellationToken);
    }

    public async Task<SpecialDateEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dynamoDBContext.LoadAsync<SpecialDateEntity>(id, cancellationToken);
    }

    public async Task<IEnumerable<SpecialDateEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dynamoDBContext.ScanAsync<SpecialDateEntity>(default).GetRemainingAsync(cancellationToken);
    }
}