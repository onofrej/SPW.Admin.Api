using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.Create;

internal sealed class CreateHandler(IUserData userData, IValidator<CreateCommand> validator) :
    IRequestHandler<CreateCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);

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

        await userData.CreateUserAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}