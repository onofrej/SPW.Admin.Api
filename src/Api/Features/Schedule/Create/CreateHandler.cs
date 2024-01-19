using SPW.Admin.Api.Features.Schedule.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Schedule.Create;

internal sealed class CreateHandler : IRequestHandler<CreateCommand, Result<Guid>>
{
    private readonly IScheduleData _scheduleData;
    private readonly IValidator<CreateCommand> _validator;

    public CreateHandler(IScheduleData scheduleData, IValidator<CreateCommand> validator)
    {
        _scheduleData = scheduleData;
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

        var entity = new ScheduleEntity
        {
            Id = Guid.NewGuid(),
            Time = request.Time
        };

        await _scheduleData.InsertAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}