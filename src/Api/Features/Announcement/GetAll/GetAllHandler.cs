using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Announcement.GetAll;

[ExcludeFromCodeCoverage]
internal sealed class GetAllHandler : IRequestHandler<GetAllQuery, Result<IEnumerable<AnnouncementEntity>>>
{
    private readonly IAnnouncementData _announcementData;

    public GetAllHandler(IAnnouncementData announcementData)
    {
        _announcementData = announcementData;
    }

    public async Task<Result<IEnumerable<AnnouncementEntity>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return new Result<IEnumerable<AnnouncementEntity>>(await _announcementData.GetAllAsync(cancellationToken));
    }
}