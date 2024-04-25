using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Announcement.Create;

internal class CreateHandler : IRequestHandler<CreateCommand, Result<Guid>>
{
    private readonly IAnnouncementData _announcementData;
    private readonly IValidator<CreateCommand> _validator;

    public CreateHandler(IAnnouncementData announcementData, IValidator<CreateCommand> validator)
    {
        _announcementData = announcementData;
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

        var entity = new AnnouncementEntity

        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Message = request.Message,
            DomainId = request.DomainId
        };

        await _announcementData.CreateAnnoucnementAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}