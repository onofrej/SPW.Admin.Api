namespace SPW.Admin.Api.Features.Authentication;

[ExcludeFromCodeCoverage]
[DynamoDBTable(TableName)]
internal sealed class AuthenticationEntity
{
    public const string TableName = "authentication";
    public const string HashKeyName = "id";

    [DynamoDBHashKey(HashKeyName)]
    public Guid Id { get; set; }

    [DynamoDBProperty("email")]
    public string? Email { get; set; }

    [DynamoDBProperty("password")]
    public string? Password { get; set; }

    [DynamoDBProperty("confirmpassword")]
    public string? ConfirmPassword { get; set; }
}