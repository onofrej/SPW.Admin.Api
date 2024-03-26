namespace SPW.Admin.Api.Features.Domain.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateValidator : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        RuleFor(expression => expression.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty");
    }
}