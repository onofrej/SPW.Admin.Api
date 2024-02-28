using SPW.Admin.Api.Features.Announcement.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Announcement.GetById;

public record GetByIdQuery(Guid Id) : IRequest<Result<AnnouncementEntity>>;