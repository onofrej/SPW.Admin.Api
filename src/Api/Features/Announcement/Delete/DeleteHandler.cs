using SPW.Admin.Api.Features.Announcement.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Announcement.Delete;

internal sealed class DeleteHandler : IRequestHandler<DeleteCommand, Result<Guid>>
{
    private readonly IAnnouncementData _announcementData;
    private readonly IValidator<DeleteCommand> _validator;

    public DeleteHandler(IAnnouncementData announcementData, IValidator<DeleteCommand> validator)
    {
        _announcementData = announcementData;
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

        var announcementEntity = await _announcementData.GetByIdAsync(request.Id, cancellationToken);

        if (announcementEntity is null)
        {
            return new Result<Guid>(Guid.Empty, Errors.ReturnAnnouncementNotFoundError());
        }

        await _announcementData.DeleteAsync(announcementEntity, cancellationToken);

        return new Result<Guid>(request.Id);
    }
}