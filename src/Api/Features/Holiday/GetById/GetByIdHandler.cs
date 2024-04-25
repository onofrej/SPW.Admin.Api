using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Holiday.GetById;

[ExcludeFromCodeCoverage]
internal sealed class GetByIdHandler : IRequestHandler<GetByIdQuery, Result<HolidayEntity>>
{
    private readonly IHolidayData _holidayData;
    private readonly IValidator<GetByIdQuery> _validator;

    public GetByIdHandler(IHolidayData holidayData, IValidator<GetByIdQuery> validator)
    {
        _holidayData = holidayData;
        _validator = validator;
    }

    public async Task<Result<HolidayEntity>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<HolidayEntity>(default, Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var holidayEntity = await _holidayData.GetHolidayByIdAsync(request.Id, cancellationToken);

        if (holidayEntity is null)
        {
            return new Result<HolidayEntity>(default, Errors.ReturnHolidayNotFoundError());
        }

        return new Result<HolidayEntity>(holidayEntity);
    }
}