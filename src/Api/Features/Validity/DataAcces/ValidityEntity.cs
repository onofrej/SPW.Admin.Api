namespace SPW.Admin.Api.Features.Validity.DataAcces;

[ExcludeFromCodeCoverage]
[DynamoDBTable(TableName)]
internal sealed class ValidityEntity
{
    public const string TableName = "validity";
    public const string HashKeyName = "id";

    [DynamoDBHashKey(HashKeyName)]
    public Guid Id { get; set; }

    [DynamoDBProperty("startdate")]
    public DateTime StartDate { get; set; }

    [DynamoDBProperty("enddate")]
    public DateTime EndDate { get; set; }

    [DynamoDBProperty("status")]
    public bool Status { get; set; }
}