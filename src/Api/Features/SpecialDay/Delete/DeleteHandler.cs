using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDay.Delete;

internal sealed class DeleteHandler : IRequestHandler<DeleteCommand, Result<Guid>>
{
    private readonly ISpecialDayData _specialDayData;
    private readonly IValidator<DeleteCommand> _validator;

    public DeleteHandler(ISpecialDayData specialDayData, IValidator<DeleteCommand> validator)
    {
        _specialDayData = specialDayData;
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

        var specialDayEntity = await _specialDayData.GetSpecialDayByIdAsync(request.Id, cancellationToken);

        if (specialDayEntity is null)
        {
            return new Result<Guid>(Guid.Empty, Errors.ReturnSpecialDayNotFoundError());
        }

        await _specialDayData.DeleteSpecialDayAsync(specialDayEntity.Id, cancellationToken);

        return new Result<Guid>(request.Id);
    }
}