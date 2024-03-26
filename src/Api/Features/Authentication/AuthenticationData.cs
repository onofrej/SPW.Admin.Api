namespace SPW.Admin.Api.Features.Authentication;

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

    public async Task RegisterAsync(AuthenticationEntity authenticationEntity, CancellationToken cancellationToken)
    {
        var document = new Document
        {
            ["id"] = authenticationEntity.Id,
            ["email"] = authenticationEntity.Email,
            ["password"] = authenticationEntity.Password,
        };

        await _table.PutItemAsync(document, cancellationToken);
    }

    public async Task<AuthenticationEntity> GetUserCredentialsAsync(string email, string password, CancellationToken cancellationToken)
    {
        return await _dynamoDBContext.LoadAsync<AuthenticationEntity>(email, password, cancellationToken);
    }
}