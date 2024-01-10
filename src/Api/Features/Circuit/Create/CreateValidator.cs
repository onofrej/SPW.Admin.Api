namespace SPW.Admin.Api.Features.Circuit.Create;

public sealed class CreateValidator : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        RuleFor(expression => expression.Name)
            .NotEmpty()
            .WithMessage("Circuit name cannot be empty");
    }
}