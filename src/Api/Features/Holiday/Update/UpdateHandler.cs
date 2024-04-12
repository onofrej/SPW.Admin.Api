using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Holiday.Update;

internal sealed class UpdateHandler : IRequestHandler<UpdateCommand, Result<Guid>>
{
    private readonly IHolidayData _holidayData;
    private readonly IValidator<UpdateCommand> _validator;

    public UpdateHandler(IHolidayData holidayData, IValidator<UpdateCommand> validator)
    {
        _holidayData = holidayData;
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

        var entity = new HolidayEntity
        {
            Id = request.Id,
            Name = request.Name,
            Date = request.Date
        };

        await _holidayData.UpdateHolidayAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}