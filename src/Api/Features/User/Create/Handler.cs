using SPW.Admin.Api.DataAccess.User;

namespace SPW.Admin.Api.Features.User;

internal sealed class Handler : IRequestHandler<Command, Guid>
{
    private readonly IUserData _userData;

    public Handler(IUserData userData)
    {
        _userData = userData;
    }

    public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
    {
        var entity = new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            CreationDate = DateTime.UtcNow
        };

        await _userData.InsertAsync(entity, cancellationToken);

        return entity.Id;
    }
}