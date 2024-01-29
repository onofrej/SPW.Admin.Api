namespace SPW.Admin.Api.Features.SpecialDate.DataAccess;

[ExcludeFromCodeCoverage]
[DynamoDBTable(TableName)]
internal sealed class SpecialDateEntity
{
    public const string TableName = "specialdate";
    public const string HashKeyName = "id";

    [DynamoDBHashKey(HashKeyName)]
    public Guid Id { get; set; }

    [DynamoDBProperty("name")]
    public string? Name { get; set; }

    [DynamoDBProperty("startdate")]
    public DateTime StartDate { get; set; }

    [DynamoDBProperty("enddate")]
    public DateTime EndDate { get; set; }

    [DynamoDBProperty("circuitId")]
    public Guid CircuitId { get; set; }
}