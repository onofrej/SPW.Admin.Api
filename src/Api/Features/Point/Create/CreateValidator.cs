﻿namespace SPW.Admin.Api.Features.Point.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateValidator : AbstractValidator<CreateCommand>
{
    public CreateValidator()
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

        RuleFor(expression => expression.DomainId)
            .NotEmpty()
            .WithMessage("Domain Id cannot be empty");
    }
}