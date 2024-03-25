using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Congregation.Delete;

[ExcludeFromCodeCoverage]
internal sealed class DeleteHandler : IRequestHandler<DeleteCommand, Result<Guid>>
{
    private readonly ICongregationData _congregationData;
    private readonly IValidator<DeleteCommand> _validator;

    public DeleteHandler(ICongregationData congregationData, IValidator<DeleteCommand> validator)
    {
        _congregationData = congregationData;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var congregationEntity = await _congregationData.GetCongregationByIdAsync(request.Id, cancellationToken);

        if (congregationEntity is null)
        {
            return new Result<Guid>(Guid.Empty, Errors.ReturnCongregationNotFoundError());
        }

        await _congregationData.DeleteCongregationAsync(congregationEntity.Id, cancellationToken);

        return new Result<Guid>(congregationEntity.Id);
    }
}