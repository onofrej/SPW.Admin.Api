namespace SPW.Admin.Api.Features.Authentication.DataAccess;

[ExcludeFromCodeCoverage]
[DynamoDBTable(TableName)]
internal class AuthenticationEntity
{
    public const string TableName = "authentication";
    public const string HashKeyName = "id";

    [DynamoDBHashKey(HashKeyName)]
    public Guid Id { get; set; }

    [DynamoDBProperty("title")]
    public string? UserName { get; set; }

    [DynamoDBProperty("message")]
    public string? Password { get; set; }
}