namespace SPW.Admin.Api.Features.Holiday.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateValidator : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        RuleFor(expression => expression.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty");

        RuleFor(expression => expression.DomainId)
           .NotEmpty()
           .WithMessage("Domain Id cannot be empty");

        //TODO: Validate date property
    }
}