namespace SPW.Admin.Api.Features.User.DataAccess;

[ExcludeFromCodeCoverage]
[DynamoDBTable(TableName)]
internal sealed class UserEntity
{
    public const string TableName = "users";
    public const string HashKeyName = "id";
    public const string SortKeyName = "name";

    [DynamoDBHashKey(HashKeyName)]
    public Guid Id { get; set; }

    [DynamoDBRangeKey(SortKeyName)]
    public string? Name { get; set; }

    [DynamoDBProperty("creation_date")]
    public DateTime CreationDate { get; set; }
}