using SPW.Admin.Api.Users.DataAccess;

namespace SPW.Admin.Api.Users.UseCases.Create;

internal sealed class UseCase : IUseCase
{
    private readonly IUserData _userData;

    public UseCase(IUserData userData)
    {
        _userData = userData;
    }

    public Task ExecuteAsync(Input input, CancellationToken cancellationToken)
    {
        return _userData.InsertAsync(new UserEntity
        {
            Id = input.Id,
            Name = input.Name,
            CreationDate = DateTime.UtcNow
        }, cancellationToken);
    }
}