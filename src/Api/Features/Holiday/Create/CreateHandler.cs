using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Holiday.Create;

internal sealed class CreateHandler : IRequestHandler<CreateCommand, Result<Guid>>
{
    private readonly IHolidayData _holidayData;
    private readonly IValidator<CreateCommand> _validator;

    public CreateHandler(IHolidayData holidayData, IValidator<CreateCommand> validator)
    {
        _holidayData = holidayData;
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

        var entity = new HolidayEntity
        {
            Date = request.Date,
            DomainId = request.DomainId,
            Id = Guid.NewGuid(),
            Name = request.Name
        };

        await _holidayData.CreateHolidayAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}