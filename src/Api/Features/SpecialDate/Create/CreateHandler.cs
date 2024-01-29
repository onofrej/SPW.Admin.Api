using SPW.Admin.Api.Features.Circuit.DataAccess;
using SPW.Admin.Api.Features.SpecialDate.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDate.Create;

internal sealed class CreateHandler : IRequestHandler<CreateCommand, Result<Guid>>
{
    private readonly ISpecialDateData _specialDateData;
    private readonly IValidator<CreateCommand> _validator;
    private readonly ICircuitData _circuitData;

    public CreateHandler(ISpecialDateData specialDateData, IValidator<CreateCommand> validator, ICircuitData circuitData)
    {
        _specialDateData = specialDateData;
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

        var entity = new SpecialDateEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CircuitId = request.CircuitId,
        };

        var queryCircuitById = await _circuitData.GetByIdAsync(request.CircuitId, cancellationToken);

        if (queryCircuitById is null)
        {
            return new Result<Guid>(Guid.Empty, Errors.ReturnCircuitNotFoundError());
        }

        await _specialDateData.InsertAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}