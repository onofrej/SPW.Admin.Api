using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Authentication.Login;

[ExcludeFromCodeCoverage]
internal sealed class LoginHandler : IRequestHandler<LoginQuery, Result<AuthenticationEntity>>
{
    private readonly IAuthenticationData _authenticationData;
    private readonly IValidator<LoginQuery> _validator;

    public LoginHandler(IAuthenticationData authenticationData, IValidator<LoginQuery> validator)
    {
        _authenticationData = authenticationData;
        _validator = validator;
    }

    public async Task<Result<AuthenticationEntity>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<AuthenticationEntity>(default, Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var authenticationEntity = await _authenticationData.GetUserCredentialsAsync(request.Email, request.Password, cancellationToken);

        return new Result<AuthenticationEntity>(authenticationEntity);
    }
}