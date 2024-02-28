namespace SPW.Admin.Api.Features.SpecialDay.Update;

public class UpdateValidator : AbstractValidator<UpdateCommand>
{
    public UpdateValidator()
    {
        RuleFor(expression => expression.Name)
           .NotEmpty()
           .WithMessage("Special day name cannot be empty");

        RuleFor(expression => expression.StartDate)
           .NotEmpty()
           .WithMessage("Special day start date cannot be empty");

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