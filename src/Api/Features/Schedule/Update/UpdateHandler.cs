using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Schedule.Update;

internal sealed class UpdateHandler(IScheduleData scheduleData, IValidator<UpdateCommand> validator) :
    IRequestHandler<UpdateCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);

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

        await scheduleData.UpdateScheduleAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}