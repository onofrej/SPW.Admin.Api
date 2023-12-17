using SPW.Admin.Api.Features.User.DataAccess;

namespace SPW.Admin.Api.Features.User.Put;

[ExcludeFromCodeCoverage]
public sealed class HandlerPut
{
    private readonly IDynamoDBContext _dynamoDBContext;

    public HandlerPut(IDynamoDBContext dynamoDBContext)
    {
        _dynamoDBContext = dynamoDBContext;
    }

    internal async Task<IResult> PutUserAsync(UserEntity userRequest)
    {
        var user = await _dynamoDBContext.LoadAsync<UserEntity>(userRequest.Id, userRequest.Name);
        var validator = new Validator();
        var validationResult = validator.Validate(userRequest);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        if (user is null)
        {
            return Results.NotFound("User not found");
        }

        await _dynamoDBContext.SaveAsync(userRequest);
        return Results.Ok(userRequest);
    }
}