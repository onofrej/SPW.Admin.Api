namespace SPW.Admin.Api.Features.Congregation.Create;

public sealed class CreateValidator : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        RuleFor(expression => expression.Name)
            .NotEmpty()
            .WithMessage("Congregation name cannot be empty");

        RuleFor(expression => expression.Number)
            .NotEmpty()
            .WithMessage("Congregation number cannot be empty")
            .Matches("^[0-9]+$")
            .WithMessage("Congregation number must be only numbers");
    }
}