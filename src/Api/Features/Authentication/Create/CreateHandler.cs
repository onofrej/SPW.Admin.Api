using SPW.Admin.Api.Features.Authentication.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Authentication.Create;

internal class CreateHandler : IRequestHandler<CreateCommand, Result<Guid>>
{
    private readonly IAuthenticationData _authenticationData;
    private readonly IValidator<CreateCommand> _validator;

    public CreateHandler(IAuthenticationData authenticationData, IValidator<CreateCommand> validator)
    {
        _authenticationData = authenticationData;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var entity = new AuthenticationEntity

        {
            Id = Guid.NewGuid(),
            UserName = request.UserName,
            Password = request.Password
        };

        await _authenticationData.InsertAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}