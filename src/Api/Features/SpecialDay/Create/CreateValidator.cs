namespace SPW.Admin.Api.Features.SpecialDay.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateValidator : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        RuleFor(expression => expression.Name)
            .NotEmpty()
            .WithMessage("Special Day name cannot be empty");

        RuleFor(expression => expression.StartDate)
           .NotEmpty()
           .WithMessage("Special day startDate cannot be empty");

        RuleFor(expression => expression.EndDate)
           .NotEmpty()
           .WithMessage("Special day end date cannot be empty")
           .GreaterThan(expression => expression.StartDate)
           .WithMessage("Special day end date cannot be lower than start date");

        RuleFor(expression => expression.CircuitId)
           .NotEmpty()
           .WithMessage("Circuit id cannot be empty");
    }
}