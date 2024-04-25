namespace SPW.Admin.Api.Features.Announcement.GetById;

[ExcludeFromCodeCoverage]
public sealed class GetByIdValidator : AbstractValidator<GetByIdQuery>
{
    public GetByIdValidator()
    {
        RuleFor(expression => expression.Id)
            .NotEmpty()
            .WithMessage("Annoucement Id cannot be empty");
    }
}