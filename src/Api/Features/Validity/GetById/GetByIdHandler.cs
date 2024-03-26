using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Validity.GetById;

[ExcludeFromCodeCoverage]
internal sealed class GetByIdHandler : IRequestHandler<GetByIdQuery, Result<ValidityEntity>>
{
    private readonly IValidityData _validityData;
    private readonly IValidator<GetByIdQuery> _validator;

    public GetByIdHandler(IValidityData validityData, IValidator<GetByIdQuery> validator)
    {
        _validityData = validityData;
        _validator = validator;
    }

    public async Task<Result<ValidityEntity>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<ValidityEntity>(default, Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var userEntity = await _validityData.GetValidityByIdAsync(request.Id, cancellationToken);

        if (userEntity is null)
        {
            return new Result<ValidityEntity>(default, Errors.ReturnValidityNotFoundError());
        }

        return new Result<ValidityEntity>(userEntity);
    }
}