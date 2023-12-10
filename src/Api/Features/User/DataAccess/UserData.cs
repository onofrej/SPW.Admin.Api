using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace SPW.Admin.Api.Features.User.DataAccess;

[ExcludeFromCodeCoverage]
internal sealed class UserData : IUserData
{
    private readonly IDynamoDBContext _dynamoDBContext;
    private readonly IAmazonDynamoDB _amazonDynamoDB;

    public UserData(IDynamoDBContext dynamoDBContext, IAmazonDynamoDB amazonDynamoDB)
    {
        _dynamoDBContext = dynamoDBContext;
        _amazonDynamoDB = amazonDynamoDB;
    }

    public Task InsertAsync(UserEntity userEntity, CancellationToken cancellationToken)
    {
        return _dynamoDBContext.SaveAsync(userEntity, cancellationToken);
    }

    public async Task InsertSingleItemAsync(UserEntity userEntity, CancellationToken cancellationToken)
    {
        var document = new Document
        {
            ["id"] = userEntity.Id,
            ["name"] = userEntity.Name,
            ["creation_date"] = userEntity.CreationDate
        };

        var putItemRequest = new PutItemRequest
        {
            TableName = userEntity.TableName,
            Item = document.ToAttributeMap()
        };

        await _amazonDynamoDB.PutItemAsync(putItemRequest, cancellationToken);
    }
}