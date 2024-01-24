namespace SPW.Admin.Api.Features.Announcement.DataAccess;

internal interface IAnnouncementData
{
    Task InsertAsync(AnnouncementEntity announcementEntity, CancellationToken cancellationToken);

    Task UpdateAsync(AnnouncementEntity announcementEntity, CancellationToken cancellationToken);

    Task DeleteAsync(AnnouncementEntity announcementEntity, CancellationToken cancellationToken);

    Task<AnnouncementEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<AnnouncementEntity>> GetAllAsync(CancellationToken cancellationToken);
}