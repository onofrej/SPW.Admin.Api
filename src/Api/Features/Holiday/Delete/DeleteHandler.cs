using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Holiday.Delete;

[ExcludeFromCodeCoverage]
internal sealed class DeleteHandler : IRequestHandler<DeleteCommand, Result<Guid>>
{
    private readonly IHolidayData _holidayData;
    private readonly IValidator<DeleteCommand> _validator;

    public DeleteHandler(IHolidayData holidayData, IValidator<DeleteCommand> validator)
    {
        _holidayData = holidayData;
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

        var holidayEntity = await _holidayData.GetByIdAsync(request.Id, cancellationToken);

        if (holidayEntity is null)
        {
            return new Result<Guid>(Guid.Empty, Errors.ReturnHolidayNotFoundError());
        }

        await _holidayData.DeleteAsync(holidayEntity, cancellationToken);

        return new Result<Guid>(request.Id);
    }
}