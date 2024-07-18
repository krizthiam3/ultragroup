using Domain.Rooms;
using Domain.Primitives;
using Domain.ValueObjects;
using Domain.DomainErrors;
using MediatR;
using ErrorOr;
using Domain.Hotels;
using Domain.RoomsTypes;


namespace Application.Rooms.Create;

public sealed class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, ErrorOr<Guid>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IRoomTypesRepository _roomTypesRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateRoomCommandHandler(IRoomRepository roomRepository, IRoomTypesRepository roomTypesRepository, IHotelRepository hotelRepository, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        _roomTypesRepository = roomTypesRepository ?? throw new ArgumentNullException(nameof(roomTypesRepository));
        _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Guid>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
    {
        /// Validatios
        if (!await _hotelRepository.ExistsAsync(new HotelId(command.HotelId)))
        {
            return Error.NotFound("Hotel.NotFound", $"The hotel with the provide Id {command.HotelId} was not found.");
        }

        if (!await _roomTypesRepository.ExistsAsync(new RoomTypesId(command.TypeId)))
        {
            return Error.NotFound("Room Types.NotFound", $"The Room Types with the provide Id {command.TypeId} was not found.");
        }


        var room = new Room(
            new RoomId(Guid.NewGuid()),
            command.Code,
            command.Name,
            command.TypeId,
            command.HotelId,
            command.Occupancy,
            command.UbicationFloor,
            command.Price,
            command.Taxes,
            true
        );

        _roomRepository.Add(room);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return room.Id.Value;
    }
}