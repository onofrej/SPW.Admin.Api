﻿namespace SPW.Admin.Api.Users.DataAccess;

[ExcludeFromCodeCoverage]
[DynamoDBTable(TableName)]
internal sealed class UserEntity
{
    public const string TableName = "users";
    public const string HashKeyName = "id";
    public const string SortKeyName = "name";

    [DynamoDBHashKey(HashKeyName)]
    public string? Id { get; set; }

    [DynamoDBRangeKey(SortKeyName)]
    public string? Name { get; set; }

    [DynamoDBProperty("creation_date")]
    public DateTime CreationDate { get; set; }
}