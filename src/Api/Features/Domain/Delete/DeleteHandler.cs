using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Domain.Delete;

[ExcludeFromCodeCoverage]
internal sealed class DeleteHandler(IDomainData domainData, IValidator<DeleteCommand> validator) : IRequestHandler<DeleteCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var domainEntity = await domainData.GetDomainByIdAsync(request.Id, cancellationToken);

        if (domainEntity is null)
        {
            return new Result<Guid>(Guid.Empty, Errors.ReturnDomainNotFoundError());
        }

        await domainData.DeleteDomainAsync(domainEntity.Id, cancellationToken);

        return new Result<Guid>(request.Id);
    }
}