using Domain.Bookings;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Bookings.Update;

internal sealed class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, ErrorOr<Unit>>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateBookingCommandHandler(IBookingRepository bookingRepository, IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(UpdateBookingCommand command, CancellationToken cancellationToken)
    {
        if (!await _bookingRepository.ExistsAsync(new BookingId(command.Id)))
        {
            return Error.NotFound("Booking.NotFound", "The booking with the provide Id was not found.");
        }

        Booking booking = Booking.UpdateBooking(command.Id,
            command.Code,
            command.CheckInDate,
            command.CheckOutDate,
            command.RoomId,
            command.CustomerId,
            command.EmergencyContactFullName,
            command.EmergencyContactPhoneNumber,
            command.Active); 

        _bookingRepository.Update(booking);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
