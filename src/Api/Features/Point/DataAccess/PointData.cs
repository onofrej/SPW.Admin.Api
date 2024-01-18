namespace SPW.Admin.Api.Features.Point.DataAccess;

[ExcludeFromCodeCoverage]
internal sealed class PointData : IPointData
{
    private readonly IDynamoDBContext _dynamoDBContext;
    private readonly IAmazonDynamoDB _amazonDynamoDBClient;
    private readonly Table _table;

    public PointData(IDynamoDBContext dynamoDBContext, IAmazonDynamoDB amazonDynamoDBClient)
    {
        _dynamoDBContext = dynamoDBContext;
        _amazonDynamoDBClient = amazonDynamoDBClient;
        _table = Table.LoadTable(_amazonDynamoDBClient, PointEntity.TableName);
    }

    public async Task InsertAsync(PointEntity pointEntity, CancellationToken cancellationToken)
    {
        var document = new Document
        {
            ["id"] = pointEntity.Id,
            ["name"] = pointEntity.Name,
            ["quantity_publishers"] = pointEntity.QuantityPublishers,
            ["address"] = pointEntity.Address,
            ["imageurl"] = pointEntity.ImageUrl,
            ["googlemaps_url"] = pointEntity.GoogleMapsUrl
        };

        await _table.PutItemAsync(document, cancellationToken);
    }

    public Task UpdateAsync(PointEntity pointEntity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(PointEntity pointEntity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<PointEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<PointEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dynamoDBContext.ScanAsync<PointEntity>(default).GetRemainingAsync(cancellationToken);
    }
}