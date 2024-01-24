namespace SPW.Admin.Api.Features.Announcement.DataAccess;

[ExcludeFromCodeCoverage]
[DynamoDBTable(TableName)]
internal sealed class AnnouncementEntity
{
    public const string TableName = "announcement";
    public const string HashKeyName = "id";

    [DynamoDBHashKey(HashKeyName)]
    public Guid Id { get; set; }

    [DynamoDBProperty("title")]
    public string? Title { get; set; }

    [DynamoDBProperty("message")]
    public string? Message { get; set; }
}