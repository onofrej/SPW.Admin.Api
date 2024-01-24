namespace SPW.Admin.Api.Features.Announcement.DataAccess;

[ExcludeFromCodeCoverage]
internal sealed class AnnouncementData : IAnnouncementData

{
    private readonly IDynamoDBContext _dynamoDBContext;
    private readonly IAmazonDynamoDB _amazonDynamoDBClient;
    private readonly Table _table;

    public AnnouncementData(IDynamoDBContext dynamoDBContext, IAmazonDynamoDB amazonDynamoDBClient)
    {
        _dynamoDBContext = dynamoDBContext;
        _amazonDynamoDBClient = amazonDynamoDBClient;
        _table = Table.LoadTable(_amazonDynamoDBClient, AnnouncementEntity.TableName);
    }

    public async Task InsertAsync(AnnouncementEntity announcementEntity, CancellationToken cancellationToken)
    {
        var document = new Document
        {
            ["id"] = announcementEntity.Id,
            ["title"] = announcementEntity.Title,
            ["message"] = announcementEntity.Message,
        };

        await _table.PutItemAsync(document, cancellationToken);
    }

    public Task UpdateAsync(AnnouncementEntity announcementEntity, CancellationToken cancellationToken)
    {
        return _dynamoDBContext.SaveAsync(announcementEntity, cancellationToken);
    }

    public Task DeleteAsync(AnnouncementEntity announcementEntity, CancellationToken cancellationToken)
    {
        return _dynamoDBContext.DeleteAsync(announcementEntity, cancellationToken);
    }

    public async Task<AnnouncementEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dynamoDBContext.LoadAsync<AnnouncementEntity>(id, cancellationToken);
    }

    public async Task<IEnumerable<AnnouncementEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dynamoDBContext.ScanAsync<AnnouncementEntity>(default).GetRemainingAsync(cancellationToken);
    }
}