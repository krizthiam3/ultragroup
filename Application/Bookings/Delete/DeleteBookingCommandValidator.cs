using FluentValidation;

namespace Application.Bookings.Delete;

public class DeleteBookingCommandValidator : AbstractValidator<DeleteBookingCommand>
{
    public DeleteBookingCommandValidator()
    {
        RuleFor(r => r.Id).NotEmpty();
    }
}