using SPW.Admin.Api.Features.SpecialDate.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDate.GetById;

public record GetByIdQuery(Guid Id) : IRequest<Result<SpecialDateEntity>>;