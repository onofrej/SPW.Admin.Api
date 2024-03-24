namespace SPW.Admin.Api.Features.SpecialDay;

[ExcludeFromCodeCoverage]
internal sealed class SpecialDayData : ISpecialDayData
{
    private readonly IDynamoDBContext _dynamoDBContext;
    private readonly IAmazonDynamoDB _amazonDynamoDBClient;
    private readonly Table _table;

    public SpecialDayData(IDynamoDBContext dynamoDBContext, IAmazonDynamoDB amazonDynamoDBClient)
    {
        _dynamoDBContext = dynamoDBContext;
        _amazonDynamoDBClient = amazonDynamoDBClient;
        _table = Table.LoadTable(_amazonDynamoDBClient, "");
    }

    public async Task InsertAsync(SpecialDayEntity specialDayEntity, CancellationToken cancellationToken)
    {
        var document = new Document
        {
            ["id"] = specialDayEntity.Id,
            ["name"] = specialDayEntity.Name,
            ["startdate"] = specialDayEntity.StartDate,
            ["enddate"] = specialDayEntity.EndDate,
            ["circuitId"] = specialDayEntity.CircuitId
        };

        await _table.PutItemAsync(document, cancellationToken);
    }

    public Task UpdateAsync(SpecialDayEntity specialDayEntity, CancellationToken cancellationToken)
    {
        return _dynamoDBContext.SaveAsync(specialDayEntity, cancellationToken);
    }

    public Task DeleteAsync(SpecialDayEntity specialDayEntity, CancellationToken cancellationToken)
    {
        return _dynamoDBContext.DeleteAsync(specialDayEntity, cancellationToken);
    }

    public async Task<SpecialDayEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dynamoDBContext.LoadAsync<SpecialDayEntity>(id, cancellationToken);
    }

    public async Task<IEnumerable<SpecialDayEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dynamoDBContext.ScanAsync<SpecialDayEntity>(default).GetRemainingAsync(cancellationToken);
    }
}