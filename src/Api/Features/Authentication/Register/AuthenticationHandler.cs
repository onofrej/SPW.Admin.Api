using SPW.Admin.Api.Features.Authentication.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Authentication.Register;

internal sealed class AuthenticationHandler : IRequestHandler<AuthenticationCommand, Result<Guid>>
{
    private readonly IAuthenticationData _authenticationData;
    private readonly IValidator<AuthenticationCommand> _validator;

    public AuthenticationHandler(IAuthenticationData authenticationData, IValidator<AuthenticationCommand> validator)
    {
        _authenticationData = authenticationData;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
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
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            ConfirmPassword = request.ConfirmPassword,
        };

        await _authenticationData.RegisterAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}