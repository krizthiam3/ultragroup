using FluentValidation;

namespace Application.Hotels.Create;

public class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
{
    public CreateHotelCommandValidator()
    {
        RuleFor(r => r.Name).NotEmpty().MaximumLength(100);

        RuleFor(r => r.Description).NotEmpty().MaximumLength(100);

        RuleFor(r => r.PhoneNumber).NotEmpty().MaximumLength(10).WithName("Phone Number");

        RuleFor(r => r.Country).NotEmpty().MaximumLength(3);

        RuleFor(r => r.City).NotEmpty().MaximumLength(100);

        RuleFor(r => r.State).NotEmpty().MaximumLength(100);

        RuleFor(r => r.ZipCode).NotEmpty().MaximumLength(100).WithName("Zip Code");
    }
}