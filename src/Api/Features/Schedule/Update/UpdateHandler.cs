using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Schedule.Update;

internal sealed class UpdateHandler : IRequestHandler<UpdateCommand, Result<Guid>>
{
    private readonly IScheduleData _scheduleData;
    private readonly IValidator<UpdateCommand> _validator;

    public UpdateHandler(IScheduleData scheduleData, IValidator<UpdateCommand> validator)
    {
        _scheduleData = scheduleData;
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

        var entity = new ScheduleEntity
        {
            Id = request.Id,
            Time = request.Time
        };

        await _scheduleData.UpdateAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}