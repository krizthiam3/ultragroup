using Domain.Rooms;
using Domain.Primitives;
using Domain.ValueObjects;
using Domain.DomainErrors;
using MediatR;
using ErrorOr;


namespace Application.Rooms.Create;

public sealed class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, ErrorOr<Guid>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateRoomCommandHandler(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Guid>> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
    {
        /// Validatios


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