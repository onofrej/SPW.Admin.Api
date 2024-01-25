using SPW.Admin.Api.Features.Announcement.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Announcement.Update;

internal sealed class UpdateHandler : IRequestHandler<UpdateCommand, Result<Guid>>
{
    private readonly IAnnouncementData _announcementData;
    private readonly IValidator<UpdateCommand> _validator;

    public UpdateHandler(IAnnouncementData announcementData, IValidator<UpdateCommand> validator)
    {
        _announcementData = announcementData;
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

        var entity = new AnnouncementEntity
        {
            Id = request.Id,
            Title = request.Title,
            Message = request.Message,
        };

        await _announcementData.UpdateAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}