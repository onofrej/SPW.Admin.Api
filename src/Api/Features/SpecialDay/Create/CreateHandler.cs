using SPW.Admin.Api.Features.Circuit;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDay.Create;

internal sealed class CreateHandler : IRequestHandler<CreateCommand, Result<Guid>>
{
    private readonly ISpecialDayData _specialDayData;
    private readonly IValidator<CreateCommand> _validator;
    private readonly ICircuitData _circuitData;

    public CreateHandler(ISpecialDayData specialDayData, IValidator<CreateCommand> validator, ICircuitData circuitData)
    {
        _specialDayData = specialDayData;
        _validator = validator;
        _circuitData = circuitData;
    }

    public async Task<Result<Guid>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var entity = new SpecialDayEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CircuitId = request.CircuitId,
        };

        var queryCircuitById = await _circuitData.GetCircuitByIdAsync(request.CircuitId, cancellationToken);

        if (queryCircuitById is null)
        {
            return new Result<Guid>(Guid.Empty, Errors.ReturnCircuitNotFoundError());
        }

        await _specialDayData.InsertAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}