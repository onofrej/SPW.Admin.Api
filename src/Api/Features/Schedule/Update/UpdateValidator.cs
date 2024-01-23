namespace SPW.Admin.Api.Features.Schedule.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateValidator : AbstractValidator<UpdateCommand>
{
    public UpdateValidator()
    {
        RuleFor(expression => expression.Time)
            .NotEmpty()
            .WithMessage("Time cannot be empty");
    }
}