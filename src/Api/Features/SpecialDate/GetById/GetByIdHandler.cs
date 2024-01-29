using SPW.Admin.Api.Features.SpecialDate.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDate.GetById;

[ExcludeFromCodeCoverage]
internal sealed class GetByIdHandler : IRequestHandler<GetByIdQuery, Result<SpecialDateEntity>>
{
    private readonly ISpecialDateData _specialDateData;
    private readonly IValidator<GetByIdQuery> _validator;

    public GetByIdHandler(ISpecialDateData specialDateData, IValidator<GetByIdQuery> validator)
    {
        _specialDateData = specialDateData;
        _validator = validator;
    }

    public async Task<Result<SpecialDateEntity>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<SpecialDateEntity>(default, Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var specialDateEntity = await _specialDateData.GetByIdAsync(request.Id, cancellationToken);

        if (specialDateEntity is null)
        {
            return new Result<SpecialDateEntity>(default, Errors.ReturnSpecialDateNotFoundError());
        }

        return new Result<SpecialDateEntity>(specialDateEntity);
    }
}