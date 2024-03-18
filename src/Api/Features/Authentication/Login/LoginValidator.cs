namespace SPW.Admin.Api.Features.Authentication.Login;

public sealed class LoginValidator : AbstractValidator<LoginQuery>
{
    public LoginValidator()
    {
        RuleFor(expression => expression.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty")
            .Matches(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$")
            .WithMessage("Invalid email address");

        RuleFor(expression => expression.Password)
            .NotEmpty()
            .WithMessage("Password cannot be empty");
    }
}