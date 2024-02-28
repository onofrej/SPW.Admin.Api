using SPW.Admin.Api.Features.Announcement.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Announcement.GetAll;

public record GetAllQuery() : IRequest<Result<IEnumerable<AnnouncementEntity>>>;