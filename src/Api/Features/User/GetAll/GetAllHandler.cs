using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.GetAll;

[ExcludeFromCodeCoverage]
internal sealed class GetAllHandler : IRequestHandler<GetAllQuery, Result<IEnumerable<PointEntity>>>
{
    private readonly IUserData _userData;

    public GetAllHandler(IUserData userData)
    {
        _userData = userData;
    }

    public async Task<Result<IEnumerable<PointEntity>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return new Result<IEnumerable<PointEntity>>(await _userData.GetAllUsersAsync(cancellationToken));
    }
}