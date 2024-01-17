using SPW.Admin.Api.Features.Circuit.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Circuit.Delete;

[ExcludeFromCodeCoverage]
internal sealed class DeleteHandler : IRequestHandler<DeleteCommand, Result<Guid>>
{
    private readonly ICircuitData _circuitData;
    private readonly IValidator<DeleteCommand> _validator;

    public DeleteHandler(ICircuitData circuitData, IValidator<DeleteCommand> validator)
    {
        _circuitData = circuitData;
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

        var circuitEntity = await _circuitData.GetByIdAsync(request.Id, cancellationToken);

        if (circuitEntity is null)
        {
            return new Result<Guid>(Guid.Empty, Errors.ReturnCircuitNotFoundError());
        }

        await _circuitData.DeleteAsync(circuitEntity, cancellationToken);

        return new Result<Guid>(circuitEntity.Id);
    }
}