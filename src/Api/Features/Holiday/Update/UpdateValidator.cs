namespace SPW.Admin.Api.Features.Holiday.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateValidator : AbstractValidator<UpdateCommand>
{
    public UpdateValidator()
    {
        RuleFor(expression => expression.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty");

        RuleFor(expression => expression.Date)
            .NotEmpty()
            .WithMessage("Data cannot be empty");
    }
}