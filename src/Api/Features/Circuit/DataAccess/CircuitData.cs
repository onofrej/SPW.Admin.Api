namespace SPW.Admin.Api.Features.Circuit.DataAccess;

internal sealed class CircuitData : ICircuitData
{
    private readonly IDynamoDBContext _dynamoDBContext;
    private readonly IAmazonDynamoDB _amazonDynamoDBClient;
    private readonly Table _table;

    public CircuitData(IDynamoDBContext dynamoDBContext, IAmazonDynamoDB amazonDynamoDBClient)
    {
        _dynamoDBContext = dynamoDBContext;
        _amazonDynamoDBClient = amazonDynamoDBClient;
        _table = Table.LoadTable(_amazonDynamoDBClient, CircuitEntity.TableName);
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

    public async Task UpdateAsync(CircuitEntity circuitEntity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(CircuitEntity circuitEntity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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