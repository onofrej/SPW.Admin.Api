using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Domain.GetById;

[ExcludeFromCodeCoverage]
internal sealed class GetByIdHandler(IDomainData domainData, IValidator<GetByIdQuery> validator) : IRequestHandler<GetByIdQuery, Result<DomainEntity>>
{
    public async Task<Result<DomainEntity>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<DomainEntity>(default, Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var domainEntity = await domainData.GetDomainByIdAsync(request.Id, cancellationToken);

        if (domainEntity is null)
        {
            return new Result<DomainEntity>(default, Errors.ReturnDomainNotFoundError());
        }

        return new Result<DomainEntity>(domainEntity);
    }
}