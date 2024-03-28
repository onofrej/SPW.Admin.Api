using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Schedule.Delete;

[ExcludeFromCodeCoverage]
internal sealed class DeleteHandler(IScheduleData scheduleData, IValidator<DeleteCommand> validator) :
    IRequestHandler<DeleteCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var scheduleEntity = await scheduleData.GetScheduleByIdAsync(request.Id, cancellationToken);

        if (scheduleEntity is null)
        {
            return new Result<Guid>(Guid.Empty, Errors.ReturnScheduleNotFoundError());
        }

        await scheduleData.DeleteScheduleAsync(request.Id, cancellationToken);

        return new Result<Guid>(request.Id);
    }
}