using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Validity.Update;

internal sealed class UpdateHandler : IRequestHandler<UpdateCommand, Result<Guid>>
{
    private readonly IValidityData _validityData;
    private readonly IValidator<UpdateCommand> _validator;

    public UpdateHandler(IValidityData validityData, IValidator<UpdateCommand> validator)
    {
        _validityData = validityData;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var entity = new ValidityEntity
        {
            Id = request.Id,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = request.Status,
            DomainId = request.DomainId,
        };

        await _validityData.UpdateValidityAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}