using FluentValidation;

namespace Application.Bookings.Update;

public class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
{
    public UpdateBookingCommandValidator()
    {
        RuleFor(r => r.Code).NotEmpty().MaximumLength(100);
        RuleFor(r => r.CheckInDate).NotEmpty();
        RuleFor(r => r.CheckInDate).NotEmpty();
        RuleFor(r => r.RoomId).NotEmpty();
        RuleFor(r => r.CustomerId).NotEmpty();
        RuleFor(r => r.EmergencyContactFullName).NotEmpty().MaximumLength(100);
        RuleFor(r => r.EmergencyContactPhoneNumber).NotEmpty().MaximumLength(100);
    }
}