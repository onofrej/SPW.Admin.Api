namespace SPW.Admin.Api.Features.Authentication.DataAccess;

[ExcludeFromCodeCoverage]
internal sealed class AuthenticationData : IAuthenticationData
{
    private readonly IDynamoDBContext _dynamoDBContext;
    private readonly IAmazonDynamoDB _amazonDynamoDBClient;
    private readonly Table _table;

    public AuthenticationData(IDynamoDBContext dynamoDBContext, IAmazonDynamoDB amazonDynamoDBClient)
    {
        _dynamoDBContext = dynamoDBContext;
        _amazonDynamoDBClient = amazonDynamoDBClient;
        _table = Table.LoadTable(_amazonDynamoDBClient, AuthenticationEntity.TableName);
    }

    public async Task InsertAsync(AuthenticationEntity authenticationEntity, CancellationToken cancellationToken)
    {
        var document = new Document
        {
            ["id"] = authenticationEntity.Id,
            ["username"] = authenticationEntity.UserName,
            ["password"] = authenticationEntity.Password,
        };

        await _table.PutItemAsync(document, cancellationToken);
    }
}