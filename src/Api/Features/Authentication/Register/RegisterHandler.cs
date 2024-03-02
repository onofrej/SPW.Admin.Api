using SPW.Admin.Api.Features.Authentication.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Authentication.Register;

internal sealed class RegisterHandler : IRequestHandler<RegisterCommand, Result<Guid>>
{
    private readonly IAuthenticationData _authenticationData;
    private readonly IValidator<RegisterCommand> _validator;

    public RegisterHandler(IAuthenticationData authenticationData, IValidator<RegisterCommand> validator)
    {
        _authenticationData = authenticationData;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
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
            Email = request.Email,
            Password = request.Password,
            ConfirmPassword = request.ConfirmPassword,
        };

        await _authenticationData.RegisterAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}