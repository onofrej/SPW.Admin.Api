namespace SPW.Admin.Api.Features.Holiday;

[ExcludeFromCodeCoverage]
internal sealed class HolidayData : IHolidayData
{
    public readonly IDynamoDBContext _dynamoDBContext;
    public readonly IAmazonDynamoDB _amazonDynamoDBClient;
    public readonly Table _table;

    public HolidayData(IDynamoDBContext dynamoDBContext, IAmazonDynamoDB amazonDynamoDBClient)
    {
        _dynamoDBContext = dynamoDBContext;
        _amazonDynamoDBClient = amazonDynamoDBClient;
        _table = Table.LoadTable(_amazonDynamoDBClient, "");
    }

    public async Task InsertAsync(HolidayEntity holidayEntity, CancellationToken cancellationToken)
    {
        var document = new Document
        {
            ["id"] = holidayEntity.Id,
            ["name"] = holidayEntity.Name,
            ["date"] = holidayEntity.Date
        };

        await _table.PutItemAsync(document, cancellationToken);
    }

    public Task UpdateAsync(HolidayEntity holidayEntity, CancellationToken cancellationToken)
    {
        return _dynamoDBContext.SaveAsync(holidayEntity, cancellationToken);
    }

    public Task DeleteAsync(HolidayEntity holidayEntity, CancellationToken cancellationToken)
    {
        return _dynamoDBContext.DeleteAsync(holidayEntity, cancellationToken);
    }

    public async Task<HolidayEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dynamoDBContext.LoadAsync<HolidayEntity>(id, cancellationToken);
    }

    public async Task<IEnumerable<HolidayEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dynamoDBContext.ScanAsync<HolidayEntity>(default).GetRemainingAsync(cancellationToken);
    }
}