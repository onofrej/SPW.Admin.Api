namespace SPW.Admin.Api.Features.Circuit.DataAccess;

[ExcludeFromCodeCoverage]
[DynamoDBTable(TableName)]
internal sealed class CircuitEntity
{
    public const string TableName = "circuit";
    public const string HashKeyName = "id";

    [DynamoDBHashKey(HashKeyName)]
    public Guid Id { get; set; }

    [DynamoDBProperty("name")]
    public string? Name { get; set; }
}