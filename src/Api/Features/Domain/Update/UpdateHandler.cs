using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Domain.Update;

internal sealed class UpdateHandler(IDomainData domainData, IValidator<UpdateCommand> validator) : IRequestHandler<UpdateCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var entity = new DomainEntity
        {
            Id = request.Id,
            Name = request.Name
        };

        await domainData.UpdateDomainAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}