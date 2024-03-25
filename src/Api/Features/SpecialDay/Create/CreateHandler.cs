using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDay.Create;

internal sealed class CreateHandler : IRequestHandler<CreateCommand, Result<Guid>>
{
    private readonly ISpecialDayData _specialDayData;
    private readonly IValidator<CreateCommand> _validator;

    public CreateHandler(ISpecialDayData specialDayData, IValidator<CreateCommand> validator)
    {
        _specialDayData = specialDayData;
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

        var entity = new SpecialDayEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CircuitId = request.CircuitId,
        };

        await _specialDayData.CreateSpecialDayAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}