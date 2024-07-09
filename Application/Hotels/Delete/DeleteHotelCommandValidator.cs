using FluentValidation;

namespace Application.Hotels.Delete;

public class DeleteHotelCommandValidator : AbstractValidator<DeleteHotelCommand>
{
    public DeleteHotelCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty();
    }
}