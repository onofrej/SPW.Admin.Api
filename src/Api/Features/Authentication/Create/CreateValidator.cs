namespace SPW.Admin.Api.Features.Authentication.Create;

public sealed class CreateValidator : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        RuleFor(expression => expression.UserName)
            .NotEmpty()
            .WithMessage("Username cannot be empty");

        RuleFor(expression => expression.Password)
            .NotEmpty()
            .WithMessage("Password cannot be empty");
    }
}