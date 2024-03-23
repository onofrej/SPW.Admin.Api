using SPW.Admin.Api.Features.User.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.GetAll;

[ExcludeFromCodeCoverage]
internal sealed class GetAllHandler : IRequestHandler<GetAllQuery, Result<IEnumerable<UserEntity>>>
{
    private readonly IUserData _userData;

    public GetAllHandler(IUserData userData)
    {
        _userData = userData;
    }

    public async Task<Result<IEnumerable<UserEntity>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return new Result<IEnumerable<UserEntity>>(await _userData.GetAllUsersAsync(cancellationToken));
    }
}