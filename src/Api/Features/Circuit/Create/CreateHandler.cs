using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Circuit.Create
{
    internal sealed class CreateHandler : IRequestHandler<CreateCommand, Result<Guid>>
    {
        private readonly ICircuitData _circuitData;
        private readonly IValidator<CreateCommand> _validator;

        public CreateHandler(ICircuitData circuitData, IValidator<CreateCommand> validator)
        {
            _circuitData = circuitData;
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

            var entity = new CircuitEntity

            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                DomainId = request.DomainId
            };

            await _circuitData.CreateCircuitAsync(entity, cancellationToken);

            return new Result<Guid>(entity.Id);
        }
    }
}