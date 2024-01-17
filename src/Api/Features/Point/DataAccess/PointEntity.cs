namespace SPW.Admin.Api.Features.Point.DataAccess;

[ExcludeFromCodeCoverage]
[DynamoDBTable(TableName)]
internal sealed class PointEntity
{
    public const string TableName = "point";
    public const string HashKeyName = "id";

    [DynamoDBHashKey(HashKeyName)]
    public Guid Id { get; set; }

    [DynamoDBProperty("name")]
    public string Name { get; set; }

    [DynamoDBProperty("quantity_publishers")]
    public int QuantityPublishers { get; set; }

    [DynamoDBProperty("address")]
    public string Asddress { get; set; }
}