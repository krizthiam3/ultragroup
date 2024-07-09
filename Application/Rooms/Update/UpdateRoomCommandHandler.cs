using Domain.Rooms;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Rooms.Update;

internal sealed class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, ErrorOr<Unit>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateRoomCommandHandler(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<ErrorOr<Unit>> Handle(UpdateRoomCommand command, CancellationToken cancellationToken)
    {
        if (!await _roomRepository.ExistsAsync(new RoomId(command.Id)))
        {
            return Error.NotFound("Room.NotFound", "The room with the provide Id was not found.");
        }

        Room room = Room.UpdateHotel(command.Id, command.Code,
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
