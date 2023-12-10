namespace SPW.Admin.Api.Features.User.DataAccess;

[ExcludeFromCodeCoverage]
[DynamoDBTable(TABLENAME)]
internal sealed class UserEntity
{
    public const string TABLENAME = "users";
    public const string HASHKEYNAME = "id";
    public const string SORTKEYNAME = "name";

    [DynamoDBHashKey(HASHKEYNAME)]
    public Guid Id { get; set; }

    [DynamoDBRangeKey(SORTKEYNAME)]
    public string? Name { get; set; }

    [DynamoDBProperty("creation_date")]
    public DateTime CreationDate { get; set; }

    public string TableName { get; } = TABLENAME;
}