namespace SPW.Admin.Api.Features.SpecialDate.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateValidator : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        RuleFor(expression => expression.Name)
            .NotEmpty()
            .WithMessage("Special Date name cannot be empty");

        RuleFor(expression => expression.StartDate)
           .NotEmpty()
           .WithMessage("Special date start date cannot be empty");

        RuleFor(expression => expression.EndDate)
           .NotEmpty()
           .WithMessage("Special date end date cannot be empty")
           .GreaterThan(expression => expression.StartDate)
           .WithMessage("Special date end date cannot be lower than start date");

        RuleFor(expression => expression.CircuitId)
           .NotEmpty()
           .WithMessage("Circuit id cannot be empty");
    }
}