namespace SPW.Admin.Api.Features.SpecialDate.Delete;

[ExcludeFromCodeCoverage]
public sealed class DeleteValidator : AbstractValidator<DeleteCommand>
{
    public DeleteValidator()
    {
        RuleFor(expression => expression.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty");
    }
}