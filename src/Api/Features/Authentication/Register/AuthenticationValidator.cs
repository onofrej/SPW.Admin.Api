namespace SPW.Admin.Api.Features.Authentication.Register;

public sealed class AuthenticationValidator : AbstractValidator<AuthenticationCommand>
{
    public AuthenticationValidator()
    {
        RuleFor(expression => expression.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty");

        RuleFor(expression => expression.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty");

        RuleFor(expression => expression.Password)
            .NotEmpty()
            .WithMessage("Password cannot be empty");

        RuleFor(expression => expression.ConfirmPassword)
            .NotEmpty()
            .WithMessage("Confirm password cannot be empty")
            .Equal(expression => expression.Password)
            .WithMessage("Passwords must be the same");
    }
}