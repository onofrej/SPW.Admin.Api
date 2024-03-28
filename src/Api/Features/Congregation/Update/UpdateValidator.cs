namespace SPW.Admin.Api.Features.Congregation.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateValidator : AbstractValidator<UpdateCommand>
{
    public UpdateValidator()
    {
        RuleFor(expression => expression.Name)
            .NotEmpty()
            .WithMessage("Congregation name cannot be empty");

        RuleFor(expression => expression.Number)
            .NotEmpty()
            .WithMessage("Congregation number cannot be empty")
            .Matches("^[0-9]+$")
            .WithMessage("Congregation number must be only numbers");

        RuleFor(expression => expression.CircuitId)
            .NotEmpty()
            .WithMessage("Circuit Id cannot be empty");
    }
}