using SPW.Admin.Api.Features.Schedule.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Schedule.GetById;

[ExcludeFromCodeCoverage]
internal sealed class GetByIdHandler : IRequestHandler<GetByIdQuery, Result<ScheduleEntity>>
{
    private readonly IScheduleData _scheduleData;
    private readonly IValidator<GetByIdQuery> _validator;

    public GetByIdHandler(IScheduleData scheduleData, IValidator<GetByIdQuery> validator)
    {
        _scheduleData = scheduleData;
        _validator = validator;
    }

    public async Task<Result<ScheduleEntity>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<ScheduleEntity>(default, Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var scheduleEntity = await _scheduleData.GetByIdAsync(request.Id, cancellationToken);

        if (scheduleEntity is null)
        {
            return new Result<ScheduleEntity>(default, Errors.ReturnScheduleNotFoundError());
        }

        return new Result<ScheduleEntity>(scheduleEntity);
    }
}