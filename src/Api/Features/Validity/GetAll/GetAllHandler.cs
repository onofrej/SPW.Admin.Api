using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Validity.GetAll;

[ExcludeFromCodeCoverage]
internal sealed class GetAllHandler : IRequestHandler<GetAllQuery, Result<IEnumerable<ValidityEntity>>>
{
    private readonly IValidityData _validityData;

    public GetAllHandler(IValidityData validityData)
    {
        _validityData = validityData;
    }

    public async Task<Result<IEnumerable<ValidityEntity>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return new Result<IEnumerable<ValidityEntity>>(await _validityData.GetAllValiditiesAsync(cancellationToken));
    }
}