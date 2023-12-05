using SPW.Admin.Api.Features.User.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.Create;

internal sealed class Handler : IRequestHandler<Command, Result<Guid>>
{
    private readonly IUserData _userData;
    private readonly IValidator<Command> _validator;

    public Handler(IUserData userData, IValidator<Command> validator)
    {
        _userData = userData;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.CreateInvalidEntriesError(validationResult.ToString()));
        }

        var entity = new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            CreationDate = DateTime.UtcNow
        };

        await _userData.InsertAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}