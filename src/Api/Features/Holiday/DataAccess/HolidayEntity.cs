namespace SPW.Admin.Api.Features.Holiday.DataAccess;

[ExcludeFromCodeCoverage]
[DynamoDBTable(TableName)]
internal sealed class HolidayEntity
{
    public const string TableName = "holiday";
    public const string HashKeyName = "id";

    [DynamoDBHashKey(HashKeyName)]
    public Guid Id { get; set; }

    [DynamoDBProperty("name")]
    public string? Name { get; set; }

    [DynamoDBProperty("date")]
    public string? Date { get; set; }
}