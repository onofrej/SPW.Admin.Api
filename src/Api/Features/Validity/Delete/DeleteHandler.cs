using SPW.Admin.Api.Features.Validity.DataAcces;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Validity.Delete;

[ExcludeFromCodeCoverage]
internal sealed class DeleteHandler : IRequestHandler<DeleteCommand, Result<Guid>>
{
    private readonly IValidityData _validityData;
    private readonly IValidator<DeleteCommand> _validator;

    public DeleteHandler(IValidityData validityData, IValidator<DeleteCommand> validator)
    {
        _validityData = validityData;
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

        var validityEntity = await _validityData.GetByIdAsync(request.Id, cancellationToken);

        if (validityEntity is null)
        {
            return new Result<Guid>(Guid.Empty, Errors.ReturnValidityNotFoundError());
        }

        await _validityData.DeleteAsync(validityEntity, cancellationToken);

        return new Result<Guid>(request.Id);
    }
}