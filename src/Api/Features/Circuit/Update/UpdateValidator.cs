namespace SPW.Admin.Api.Features.Circuit.Update;

public sealed class UpdateValidator : AbstractValidator<UpdateCommand>
{
    public UpdateValidator()
    {
        RuleFor(expression => expression.Name)
            .NotEmpty()
            .WithMessage("Circuit name cannot be empry");

        RuleFor(expression => expression.DomainId)
            .NotEmpty()
            .WithMessage("Domain Id cannot be empty");
    }
}