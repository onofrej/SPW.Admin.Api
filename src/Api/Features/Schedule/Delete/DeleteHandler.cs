using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Schedule.Delete;

[ExcludeFromCodeCoverage]
internal sealed class DeleteHandler : IRequestHandler<DeleteCommand, Result<Guid>>
{
    private readonly IScheduleData _scheduleData;
    private readonly IValidator<DeleteCommand> _validator;

    public DeleteHandler(IScheduleData scheduleData, IValidator<DeleteCommand> validator)
    {
        _scheduleData = scheduleData;
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

        var scheduleEntity = await _scheduleData.GetByIdAsync(request.Id, cancellationToken);

        if (scheduleEntity is null)
        {
            return new Result<Guid>(Guid.Empty, Errors.ReturnScheduleNotFoundError());
        }

        await _scheduleData.DeleteAsync(scheduleEntity, cancellationToken);

        return new Result<Guid>(request.Id);
    }
}