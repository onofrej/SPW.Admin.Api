namespace SPW.Admin.Api.Features.Congregation.GetById;

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