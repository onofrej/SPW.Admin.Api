using SPW.Admin.Api.Features.User.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.Read;

[ExcludeFromCodeCoverage]
internal sealed class GetByIdHandler : IRequestHandler<ReadCommand, Result<Guid>>
{
    private readonly IUserData _userData;
    private readonly IValidator<ReadCommand> _validator;

    public GetByIdHandler(IUserData userData, IValidator<ReadCommand> validator)
    {
        _userData = userData;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(ReadCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var userEntityById = await _userData.GetByIdAsync(request.Id, cancellationToken);

        if (userEntityById is null)
        {
            return new Result<Guid>(Guid.Empty, Errors.ReturnUserNotFoundError());
        }

        request.Name = userEntityById.Name!;
        request.Email = userEntityById.Email!;
        request.PhoneNumber = userEntityById.PhoneNumber!;
        request.Gender = userEntityById.Gender!;
        request.BirthDate = userEntityById.BirthDate!;
        request.BaptismDate = userEntityById.BaptismDate!;
        request.Privilege = userEntityById.Privilege!;

        return new Result<Guid>(request.Id);
    }
}