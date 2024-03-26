using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.GetById;

[ExcludeFromCodeCoverage]
internal sealed class GetByIdHandler : IRequestHandler<GetByIdQuery, Result<PointEntity>>
{
    private readonly IUserData _userData;
    private readonly IValidator<GetByIdQuery> _validator;

    public GetByIdHandler(IUserData userData, IValidator<GetByIdQuery> validator)
    {
        _userData = userData;
        _validator = validator;
    }

    public async Task<Result<PointEntity>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<PointEntity>(default, Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var userEntity = await _userData.GetUserByIdAsync(request.Id, cancellationToken);

        if (userEntity is null)
        {
            return new Result<PointEntity>(default, Errors.ReturnUserNotFoundError());
        }

        return new Result<PointEntity>(userEntity);
    }
}