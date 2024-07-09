using Domain.Hotels;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Hotels.Delete;

internal sealed class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, ErrorOr<Unit>>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteHotelCommandHandler(IHotelRepository hotelRepository, IUnitOfWork unitOfWork)
    {
        _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteHotelCommand command, CancellationToken cancellationToken)
    {
        if (await _hotelRepository.GetByIdAsync(new HotelId(command.Id)) is not Hotel hotel)
        {
            return Error.NotFound("Customer.NotFound", "The hotel with the provide Id was not found.");
        }

        _hotelRepository.Delete(hotel);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
