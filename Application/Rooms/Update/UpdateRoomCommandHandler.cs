using Domain.Rooms;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Domain.Hotels;
using Domain.RoomsTypes;

namespace Application.Rooms.Update;

internal sealed class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, ErrorOr<Unit>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IRoomTypesRepository _roomTypesRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateRoomCommandHandler(IRoomRepository roomRepository, IRoomTypesRepository roomTypesRepository, IHotelRepository hotelRepository, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        _roomTypesRepository = roomTypesRepository ?? throw new ArgumentNullException(nameof(roomTypesRepository));
        _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateRoomCommand command, CancellationToken cancellationToken)
    {
        if (!await _roomRepository.ExistsAsync(new RoomId(command.Id)))
        {
            return Error.NotFound("Room.NotFound", "The room with the provide Id was not found.");
        }

        /// Validatios
        if (!await _hotelRepository.ExistsAsync(new HotelId(command.HotelId)))
        {
            return Error.NotFound("Hotel.NotFound", $"The hotel with the provide Id {command.HotelId} was not found.");
        }

        if (!await _roomTypesRepository.ExistsAsync(new RoomTypesId(command.TypeId)))
        {
            return Error.NotFound("Room Types.NotFound", $"The Room Types with the provide Id {command.TypeId} was not found.");
        }


        Room room = Room.Update(command.Id, command.Code,
            command.Name,
            command.TypeId,
            command.HotelId,
            command.Occupancy,
            command.UbicationFloor,
            command.Price,
            command.Taxes,
            command.Active); 

        _roomRepository.Update(room);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
