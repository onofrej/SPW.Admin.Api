namespace SPW.Admin.Api.Features.User.Read;

[ExcludeFromCodeCoverage]
public sealed class ReadValidator : AbstractValidator<ReadCommand>
{
    public ReadValidator()
    {
        RuleFor(expression => expression.Id)
               .NotEmpty()
               .WithMessage("Id cannot be empty");
    }
}