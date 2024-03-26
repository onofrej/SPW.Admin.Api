using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Domain.GetAll;

[ExcludeFromCodeCoverage]
internal sealed class GetAllHandler(IDomainData domainData) : IRequestHandler<GetAllQuery, Result<IEnumerable<DomainEntity>>>
{
    public async Task<Result<IEnumerable<DomainEntity>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return new Result<IEnumerable<DomainEntity>>(await domainData.GetAllDomainsAsync(cancellationToken));
    }
}