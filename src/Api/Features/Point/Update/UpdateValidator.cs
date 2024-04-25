namespace SPW.Admin.Api.Features.Point.Update;

[ExcludeFromCodeCoverage]
public class UpdateValidator : AbstractValidator<UpdateCommand>
{
    public UpdateValidator()
    {
        RuleFor(expression => expression.Name)
           .NotEmpty()
           .WithMessage("Point name cannot be empty");

        RuleFor(expression => expression.NumberOfPublishers)
            .NotEmpty()
            .WithMessage("Number of publishers cannot be empty")
            .GreaterThan(0)
            .WithMessage("Number of publishers cannot be less than zero");

        RuleFor(expression => expression.Address)
            .NotEmpty()
            .WithMessage("Point address cannot be empty");

        RuleFor(expression => expression.ImageUrl)
            .NotEmpty()
            .WithMessage("Image Url cannot be empty");

        RuleFor(expression => expression.GoogleMapsUrl)
            .NotEmpty()
            .WithMessage("Google Maps Url cannot be empty");
    }
}