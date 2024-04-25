using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Circuit.GetById;

[ExcludeFromCodeCoverage]
internal sealed class GetByIdHandler : IRequestHandler<GetByIdQuery, Result<CircuitEntity>>
{
    private readonly ICircuitData _circuitData;
    private readonly IValidator<GetByIdQuery> _validator;

    public GetByIdHandler(ICircuitData circuitData, IValidator<GetByIdQuery> validator)
    {
        _circuitData = circuitData;
        _validator = validator;
    }

    public async Task<Result<CircuitEntity>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<CircuitEntity>(default, Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }
        var circuitEntity = await _circuitData.GetCircuitByIdAsync(request.Id, cancellationToken);

        if (circuitEntity is null)
        {
            return new Result<CircuitEntity>(default, Errors.ReturnCircuitNotFoundError());
        }

        return new Result<CircuitEntity>(circuitEntity);
    }
}