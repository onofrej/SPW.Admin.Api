using SPW.Admin.Api.Features.SpecialDate.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDate.GetAll;

[ExcludeFromCodeCoverage]
internal sealed class GetAllHandler : IRequestHandler<GetAllQuery, Result<IEnumerable<SpecialDateEntity>>>
{
    private readonly ISpecialDateData _specialDateData;

    public GetAllHandler(ISpecialDateData specialDateData)
    {
        _specialDateData = specialDateData;
    }

    public async Task<Result<IEnumerable<SpecialDateEntity>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return new Result<IEnumerable<SpecialDateEntity>>(await _specialDateData.GetAllAsync(cancellationToken));
    }
}