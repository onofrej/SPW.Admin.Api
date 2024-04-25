namespace SPW.Admin.Api.Features.Schedule.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateValidator : AbstractValidator<UpdateCommand>
{
    public UpdateValidator()
    {
        RuleFor(expression => expression.Time)
            .NotEmpty()
            .WithMessage("Time cannot be empty")
            .Matches("^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]-(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$")
            .WithMessage("Time is invalid - should be in this format: HH:MM-HH:MM");
    }
}