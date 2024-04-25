using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Schedule.GetById;

[ExcludeFromCodeCoverage]
internal sealed class GetByIdHandler(IScheduleData scheduleData, IValidator<GetByIdQuery> validator) :
    IRequestHandler<GetByIdQuery, Result<ScheduleEntity>>
{
    public async Task<Result<ScheduleEntity>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<ScheduleEntity>(default, Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var scheduleEntity = await scheduleData.GetScheduleByIdAsync(request.Id, cancellationToken);

        if (scheduleEntity is null)
        {
            return new Result<ScheduleEntity>(default, Errors.ReturnScheduleNotFoundError());
        }

        return new Result<ScheduleEntity>(scheduleEntity);
    }
}