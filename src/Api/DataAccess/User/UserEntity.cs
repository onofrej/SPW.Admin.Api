﻿namespace SPW.Admin.Api.DataAccess.User;

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

    [DynamoDBProperty("email")]
    public string? Email { get; set; }

    [DynamoDBProperty("phonenumber")]
    public string? PhoneNumber { get; set; }

    [DynamoDBProperty("gender")]
    public string? Gender { get; set; }

    [DynamoDBProperty("birthdate")]
    public DateTime BirthDate { get; set; }

    [DynamoDBProperty("baptismdate")]
    public DateTime BaptismDate { get; set; }

    [DynamoDBProperty("privilege")]
    public string? Privilege { get; set; }
}