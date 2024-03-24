using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.Delete;

[ExcludeFromCodeCoverage]
internal sealed class DeleteHandler : IRequestHandler<DeleteCommand, Result<Guid>>
{
    private readonly IUserData _userData;
    private readonly IValidator<DeleteCommand> _validator;

    public DeleteHandler(IUserData userData, IValidator<DeleteCommand> validator)
    {
        _userData = userData;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var userEntity = await _userData.GetUserByIdAsync(request.Id, cancellationToken);

        if (userEntity is null)
        {
            return new Result<Guid>(Guid.Empty, Errors.ReturnUserNotFoundError());
        }

        await _userData.DeleteUserAsync(userEntity.Id, cancellationToken);

        return new Result<Guid>(request.Id);
    }
}