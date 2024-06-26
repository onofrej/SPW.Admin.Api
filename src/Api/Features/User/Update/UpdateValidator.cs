﻿namespace SPW.Admin.Api.Features.User.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateValidator : AbstractValidator<UpdateCommand>
{
    public UpdateValidator()
    {
        RuleFor(expression => expression.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty");

        RuleFor(expression => expression.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty")
            .Matches(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$")
            .WithMessage("Invalid email address");

        RuleFor(expression => expression.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone Number cannot be empty")
            .Matches("^[0-9]*$")
            .WithMessage("You must just enter numbers")
            .Length(11)
            .WithMessage("The phone number length must be 11");

        RuleFor(expression => expression.Gender)
            .NotEmpty()
            .WithMessage("Gender cannot be empty");

        RuleFor(expression => expression.BirthDate)
            .NotEmpty()
            .WithMessage("Birth Date cannot be empty");

        RuleFor(expression => expression.BaptismDate)
            .NotEmpty()
            .WithMessage("Baptism Date cannot be empty");

        RuleFor(expression => expression.Privilege)
            .NotEmpty()
            .WithMessage("Privilege cannot be empty");
    }
}