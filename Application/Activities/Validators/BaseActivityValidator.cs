using System;
using Application.Activities.DTOs;
using FluentValidation;

namespace Application.Activities.Validators;

//T is the type being validated
//TDto is the actual object containing the properties to validate
public class BaseActivityValidator<T, TDto> : AbstractValidator<T> where TDto : BaseActivityDto
{
    // Constructor that takes a selector function to access the DTO properties
    //Function<T, TDto> is a delegate representing function that takes an instance of T and returns an instance of TDto
    public BaseActivityValidator(Func<T, TDto> selector)
    {
        RuleFor(x => selector(x).Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters");

        RuleFor(x => selector(x).Date)
            .NotEmpty().WithMessage("Date is required")
            .GreaterThan(DateTime.UtcNow).WithMessage("Date must be in the future");

        RuleFor(x => selector(x).Description)
            .NotEmpty()
            .WithMessage("Description is required");

        RuleFor(x => selector(x).Category)
            .NotEmpty()
            .WithMessage("Category is required");

        RuleFor(x => selector(x).City)
            .NotEmpty()
            .WithMessage("City is required");

        RuleFor(x => selector(x).Venue)
            .NotEmpty()
            .WithMessage("Venue is required");

        RuleFor(x => selector(x).Latitude)
            .NotEmpty().WithMessage("Latitude is required")
            .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90");

        RuleFor(x => selector(x).Longitude)
            .NotEmpty().WithMessage("Longitude is required")
            .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180");
    }
}
