namespace SPW.Admin.Api.Features.Announcement.Create;

public sealed class CreateValidator : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        RuleFor(expression => expression.Title)
            .NotEmpty()
            .WithMessage("Annoucement title cannot be empty");

        RuleFor(expression => expression.Message)
            .NotEmpty()
            .WithMessage("Annoucement message cannot be empty");
    }
}