using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Domain.Create;

internal sealed class CreateHandler(IDomainData domainData, IValidator<CreateCommand> validator) : IRequestHandler<CreateCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var entity = new DomainEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };

        await domainData.CreateDomainAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}