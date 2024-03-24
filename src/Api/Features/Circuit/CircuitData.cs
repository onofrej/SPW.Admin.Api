namespace SPW.Admin.Api.Features.Circuit;

internal sealed class CircuitData : ICircuitData
{
    private readonly IDynamoDBContext _dynamoDBContext;
    private readonly IAmazonDynamoDB _amazonDynamoDBClient;
    private readonly Table _table;

    public CircuitData(IDynamoDBContext dynamoDBContext, IAmazonDynamoDB amazonDynamoDBClient)
    {
        _dynamoDBContext = dynamoDBContext;
        _amazonDynamoDBClient = amazonDynamoDBClient;
        _table = Table.LoadTable(_amazonDynamoDBClient, "");
    }

    public async Task InsertAsync(CircuitEntity circuitEntity, CancellationToken cancellationToken)
    {
        var document = new Document
        {
            ["id"] = circuitEntity.Id,
            ["name"] = circuitEntity.Name
        };

        await _table.PutItemAsync(document, cancellationToken);
    }

    public Task UpdateAsync(CircuitEntity circuitEntity, CancellationToken cancellationToken)
    {
        return _dynamoDBContext.SaveAsync(circuitEntity, cancellationToken);
    }

    public Task DeleteAsync(CircuitEntity circuitEntity, CancellationToken cancellationToken)
    {
        return _dynamoDBContext.DeleteAsync(circuitEntity, cancellationToken);
    }

    public async Task<CircuitEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dynamoDBContext.LoadAsync<CircuitEntity>(id, cancellationToken);
    }

    public async Task<IEnumerable<CircuitEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dynamoDBContext.ScanAsync<CircuitEntity>(default).GetRemainingAsync(cancellationToken);
    }
}