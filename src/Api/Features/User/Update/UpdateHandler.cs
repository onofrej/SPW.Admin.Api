using SPW.Admin.Api.Features.User.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.Update;

internal sealed class UpdateHandler : IRequestHandler<UpdateCommand, Result<Guid>>
{
    private readonly IUserData _userData;
    private readonly IValidator<UpdateCommand> _validator;

    public UpdateHandler(IUserData userData, IValidator<UpdateCommand> validator)
    {
        _userData = userData;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                UpdateErrors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var entity = new UserEntity
        {
            Id = request.Id,
            Name = request.Name,
            CreationDate = DateTime.UtcNow,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Gender = request.Gender,
            BirthDate = request.BirthDate,
            BaptismDate = request.BaptismDate,
            Privilege = request.Privilege
        };

        await _userData.UpdateAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}