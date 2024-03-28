

namespace SPW.Admin.Api.Features.Announcement;

internal interface IAnnouncementData
{
    Task<int> CreateAnnoucnementAsync(AnnouncementEntity announcement, CancellationToken cancellationToken);
    Task<int> DeleteAnnouncementAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<AnnouncementEntity>> GetAllAnnouncementsAsync(CancellationToken cancellationToken);
    Task<AnnouncementEntity> GetAnnouncementByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<int> UpdateAnnoucementAsync(AnnouncementEntity announcement, CancellationToken cancellationToken);
}