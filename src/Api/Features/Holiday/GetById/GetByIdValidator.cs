namespace SPW.Admin.Api.Features.Holiday.GetById;

[ExcludeFromCodeCoverage]
public sealed class GetByIdValidator : AbstractValidator<GetByIdQuery>
{
    public GetByIdValidator()
    {
        RuleFor(expression => expression.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty");
    }
}