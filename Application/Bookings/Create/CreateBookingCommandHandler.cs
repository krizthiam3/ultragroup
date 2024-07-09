using Domain.Bookings;
using Domain.Primitives;
using Domain.ValueObjects;
using Domain.DomainErrors;
using MediatR;
using ErrorOr;


namespace Application.Bookings.Create;

public sealed class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, ErrorOr<Guid>>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateBookingCommandHandler(IBookingRepository bookingRepository, IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Guid>> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
    {
        /// Validatios


        var booking = new Booking(
            new BookingId(Guid.NewGuid()),
            command.Code,
            command.CheckInDate,
            command.CheckOutDate,
            command.RoomId,
            command.CustomerId,
            command.EmergencyContactFullName,
            command.EmergencyContactPhoneNumber,
            true
        );

        _bookingRepository.Add(booking);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return booking.Id.Value;
    }
}