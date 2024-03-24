using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Announcement.GetById;

[ExcludeFromCodeCoverage]
internal sealed class GetByIdHandler : IRequestHandler<GetByIdQuery, Result<AnnouncementEntity>>
{
    private readonly IAnnouncementData _announcementData;
    private readonly IValidator<GetByIdQuery> _validator;

    public GetByIdHandler(IAnnouncementData announcementData, IValidator<GetByIdQuery> validator)
    {
        _announcementData = announcementData;
        _validator = validator;
    }

    public async Task<Result<AnnouncementEntity>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<AnnouncementEntity>(default, Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var announcementEntity = await _announcementData.GetByIdAsync(request.Id, cancellationToken);

        if (announcementEntity is null)
        {
            return new Result<AnnouncementEntity>(default, Errors.ReturnAnnouncementNotFoundError());
        }

        return new Result<AnnouncementEntity>(announcementEntity);
    }
}