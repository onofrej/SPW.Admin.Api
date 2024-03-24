using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDay.GetById;

[ExcludeFromCodeCoverage]
internal sealed class GetByIdHandler : IRequestHandler<GetByIdQuery, Result<SpecialDayEntity>>
{
    private readonly ISpecialDayData _specialDayData;
    private readonly IValidator<GetByIdQuery> _validator;

    public GetByIdHandler(ISpecialDayData specialDayData, IValidator<GetByIdQuery> validator)
    {
        _specialDayData = specialDayData;
        _validator = validator;
    }

    public async Task<Result<SpecialDayEntity>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<SpecialDayEntity>(default, Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var specialDayEntity = await _specialDayData.GetByIdAsync(request.Id, cancellationToken);

        if (specialDayEntity is null)
        {
            return new Result<SpecialDayEntity>(default, Errors.ReturnSpecialDayNotFoundError());
        }

        return new Result<SpecialDayEntity>(specialDayEntity);
    }
}