using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDay.Update;

internal sealed class UpdateHandler : IRequestHandler<UpdateCommand, Result<Guid>>
{
    private readonly ISpecialDayData _specialDayData;
    private readonly IValidator<UpdateCommand> _validator;

    public UpdateHandler(ISpecialDayData specialDayData, IValidator<UpdateCommand> validator)
    {
        _specialDayData = specialDayData;
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

        var entity = new SpecialDayEntity
        {
            Id = request.Id,
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CircuitId = request.CircuitId,
        };

        await _specialDayData.UpdateSpecialDayAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}