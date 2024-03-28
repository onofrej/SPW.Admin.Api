﻿using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Validity.Create;

internal sealed class CreateHandler : IRequestHandler<CreateCommand, Result<Guid>>
{
    private readonly IValidityData _validityData;
    private readonly IValidator<CreateCommand> _validator;

    public CreateHandler(IValidityData validityData, IValidator<CreateCommand> validator)
    {
        _validityData = validityData;
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

        var entity = new ValidityEntity
        {
            Id = Guid.NewGuid(),
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = true,
            DomainId = request.DomainId
        };

        await _validityData.CreateValidityAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}