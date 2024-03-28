using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.Create;

internal sealed class CreateHandler : IRequestHandler<CreateCommand, Result<Guid>>
{
    private readonly IUserData _userData;
    private readonly IValidator<CreateCommand> _validator;

    public CreateHandler(IUserData userData, IValidator<CreateCommand> validator)
    {
        _userData = userData;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var entity = new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            CreationDate = DateTime.UtcNow,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Gender = request.Gender,
            BirthDate = request.BirthDate,
            BaptismDate = request.BaptismDate,
            Privilege = request.Privilege,
            CongregationId = request.CongregationId
        };

        await _userData.CreateUserAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}