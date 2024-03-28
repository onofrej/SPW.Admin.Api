using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Circuit.Update;

internal sealed class UpdateHandler : IRequestHandler<UpdateCommand, Result<Guid>>
{
    private readonly ICircuitData _circuitData;
    private readonly IValidator<UpdateCommand> _validator;

    public UpdateHandler(ICircuitData circuitData, IValidator<UpdateCommand> validator)
    {
        _circuitData = circuitData;
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

        var entity = new CircuitEntity
        {
            Id = request.Id,
            Name = request.Name,
        };

        await _circuitData.UpdateCircuitAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}