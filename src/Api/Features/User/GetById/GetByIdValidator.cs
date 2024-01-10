namespace SPW.Admin.Api.Features.User.Read;

[ExcludeFromCodeCoverage]
public sealed class GetByIdValidator : AbstractValidator<ReadCommand>
{
    public GetByIdValidator()
    {
        RuleFor(expression => expression.Id)
               .NotEmpty()
               .WithMessage("Id cannot be empty");
    }
}