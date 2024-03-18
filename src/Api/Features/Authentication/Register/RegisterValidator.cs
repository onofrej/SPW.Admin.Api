namespace SPW.Admin.Api.Features.Authentication.Register;

public sealed class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(expression => expression.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty")
            .Matches(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$")
            .WithMessage("Invalid email address");

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