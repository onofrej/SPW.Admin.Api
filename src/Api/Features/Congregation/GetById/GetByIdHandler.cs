using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Congregation.GetById;

[ExcludeFromCodeCoverage]
internal sealed class GetByIdHandler : IRequestHandler<GetByIdQuery, Result<CongregationEntity>>
{
    private readonly ICongregationData _congregationData;
    private readonly IValidator<GetByIdQuery> _validator;

    public GetByIdHandler(ICongregationData congregationData, IValidator<GetByIdQuery> validator)
    {
        _congregationData = congregationData;
        _validator = validator;
    }

    public async Task<Result<CongregationEntity>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<CongregationEntity>(default, Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }
        var congregationEntity = await _congregationData.GetCongregationByIdAsync(request.Id, cancellationToken);

        if (congregationEntity is null)
        {
            return new Result<CongregationEntity>(default, Errors.ReturnCongregationNotFoundError());
        }

        return new Result<CongregationEntity>(congregationEntity);
    }
}