namespace SPW.Admin.Api.Features.Validity.Create;

public sealed class CreateValidator : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        RuleFor(expression => expression.StartDate)
            .NotEmpty()
            .WithMessage("Start date cannot be empty");

        RuleFor(expression => expression.EndDate)
           .NotEmpty()
           .WithMessage("End date cannot be empty")
           .GreaterThan(expression => expression.StartDate)
           .WithMessage("End date cannot be less than Start Date");
    }
}