namespace SPW.Admin.Api.Features.Point.DataAccess;

[ExcludeFromCodeCoverage]
internal sealed class PointData : IPointData
{
    private readonly IDynamoDBContext _dynamoDBContext;
    private readonly IAmazonDynamoDB _amazonDynamoDBClient;
    private readonly Table _table;

    public PointData(IDynamoDBContext dynamoDBContext, IAmazonDynamoDB amazonDynamoDBClient, Table table)
    {
        _dynamoDBContext = dynamoDBContext;
        _amazonDynamoDBClient = amazonDynamoDBClient;
        _table = table;
    }

    public Task InsertAsyn(PointEntity pointEntity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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

    public Task<IEnumerable<PointEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}