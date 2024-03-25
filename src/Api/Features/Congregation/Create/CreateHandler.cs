using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Congregation.Create;

internal sealed class CreateHandler : IRequestHandler<CreateCommand, Result<Guid>>
{
    private readonly ICongregationData _congregationData;
    private readonly IValidator<CreateCommand> _validator;

    public CreateHandler(ICongregationData congregationData, IValidator<CreateCommand> validator)
    {
        _congregationData = congregationData;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var entity = new CongregationEntity

        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Number = request.Number,
            CircuitId = request.CircuitId,
        };

        await _congregationData.CreateCongregationAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}