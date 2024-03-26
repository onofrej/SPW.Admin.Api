namespace SPW.Admin.Api.Features.Validity.Update;

[ExcludeFromCodeCoverage]
public class UpdateValidator : AbstractValidator<UpdateCommand>
{
    public UpdateValidator()
    {
        RuleFor(expression => expression.StartDate)
            .NotEmpty()
            .WithMessage("Start date cannot be empty");

        RuleFor(expression => expression.EndDate)
           .NotEmpty()
           .WithMessage("End date cannot be empty")
           .GreaterThan(expression => expression.StartDate)
           .WithMessage("End date cannot be lower than Start Date");

        RuleFor(expression => expression.DomainId)
           .NotEmpty()
           .WithMessage("Domain Id cannot be empty");
    }
}