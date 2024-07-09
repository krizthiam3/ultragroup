using Domain.Bookings;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Bookings.Delete;

internal sealed class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, ErrorOr<Unit>>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteBookingCommandHandler(IBookingRepository bookingRepository, IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteBookingCommand command, CancellationToken cancellationToken)
    {
        if (await _bookingRepository.GetByIdAsync(new BookingId(command.Id)) is not Booking booking)
        {
            return Error.NotFound("Boking.NotFound", "The booking with the provide Id was not found.");
        }

        _bookingRepository.Delete(booking);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
