using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Congregation.Update;

internal sealed class UpdateHandler : IRequestHandler<UpdateCommand, Result<Guid>>
{
    private readonly ICongregationData _congregationData;
    private readonly IValidator<UpdateCommand> _validator;

    public UpdateHandler(ICongregationData congregationData, IValidator<UpdateCommand> validator)
    {
        _congregationData = congregationData;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var entity = new CongregationEntity
        {
            Id = request.Id,
            Name = request.Name,
            Number = request.Number,
            CircuitId = request.CircuitId
        };

        await _congregationData.UpdateCongregationAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}