namespace SPW.Admin.Api.Features.Announcement.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateValidator : AbstractValidator<UpdateCommand>
{
    public UpdateValidator()
    {
        RuleFor(expression => expression.Title)
            .NotEmpty()
            .WithMessage("Annoucement title cannot be empty");

        RuleFor(expression => expression.Message)
            .NotEmpty()
            .WithMessage("Annoucement message cannot be empty");
    }
}