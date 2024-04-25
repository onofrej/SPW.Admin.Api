using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Schedule.Create;

internal sealed class CreateHandler(IScheduleData scheduleData, IValidator<CreateCommand> validator) :
    IRequestHandler<CreateCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var entity = new ScheduleEntity
        {
            Id = Guid.NewGuid(),
            DomainId = request.DomainId,
            Time = request.Time
        };

        await scheduleData.CreateScheduleAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}