namespace SPW.Admin.Api.Features.Announcement.Delete;

public sealed class DeleteValidator : AbstractValidator<DeleteCommand>
{
    public DeleteValidator()
    {
        RuleFor(expression => expression.Id)
            .NotEmpty()
            .WithMessage("Announcement Id cannot be empty");
    }
}