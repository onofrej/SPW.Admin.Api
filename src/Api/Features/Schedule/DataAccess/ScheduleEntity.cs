namespace SPW.Admin.Api.Features.Schedule.DataAccess;

[ExcludeFromCodeCoverage]
[DynamoDBTable(TableName)]
internal sealed class ScheduleEntity
{
    public const string TableName = "schedule";
    public const string HashKeyName = "id";

    [DynamoDBHashKey(HashKeyName)]
    public Guid Id { get; set; }

    [DynamoDBHashKey("time")]
    public DateTime Time { get; set; }

    [DynamoDBHashKey("timeCode")]
    public string? TimeCode { get; private set; }
}