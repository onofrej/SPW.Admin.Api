namespace SPW.Admin.Api.Features.Schedule;

[ExcludeFromCodeCoverage]
internal sealed class ScheduleData : IScheduleData
{
    private readonly IDynamoDBContext _dynamoDBContext;
    private readonly IAmazonDynamoDB _amazonDynamoDBClient;
    private readonly Table _table;

    public ScheduleData(IDynamoDBContext dynamoDBContext, IAmazonDynamoDB amazonDynamoDBClient)
    {
        _dynamoDBContext = dynamoDBContext;
        _amazonDynamoDBClient = amazonDynamoDBClient;
        _table = Table.LoadTable(_amazonDynamoDBClient, "");
    }

    public async Task InsertAsync(ScheduleEntity scheduleEntity, CancellationToken cancellationToken)
    {
        var document = new Document
        {
            ["id"] = scheduleEntity.Id,
            ["time"] = scheduleEntity.Time
        };

        await _table.PutItemAsync(document, cancellationToken);
    }

    public Task UpdateAsync(ScheduleEntity scheduleEntity, CancellationToken cancellationToken)
    {
        return _dynamoDBContext.SaveAsync(scheduleEntity, cancellationToken);
    }

    public Task DeleteAsync(ScheduleEntity scheduleEntity, CancellationToken cancellationToken)
    {
        return _dynamoDBContext.DeleteAsync(scheduleEntity, cancellationToken);
    }

    public async Task<ScheduleEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dynamoDBContext.LoadAsync<ScheduleEntity>(id, cancellationToken);
    }

    public async Task<IEnumerable<ScheduleEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dynamoDBContext.ScanAsync<ScheduleEntity>(default).GetRemainingAsync(cancellationToken);
    }
}