using SPW.Admin.Api.Features.User.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.GetById
{
    public record GetByIdQuery(Guid Id) : IRequest<Result<UserEntity>>;
}