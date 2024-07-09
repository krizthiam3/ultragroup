using Rooms.Common;
using Domain.Rooms;
using ErrorOr;
using MediatR;

namespace Application.Rooms.GetById;


internal sealed class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, ErrorOr<RoomResponse>>
{
    private readonly IRoomRepository _roomRepository;

    public GetRoomByIdQueryHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
    }

    public async Task<ErrorOr<RoomResponse>> Handle(GetRoomByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _roomRepository.GetByIdAsync(new RoomId(query.Id)) is not Room room)
        {
            return Error.NotFound("Room.NotFound", "The room with the provide Id was not found.");
        }

        return new RoomResponse(
        room.Id.Value,
        room.Code,
        room.Name,
             room.TypeId,
             room.HotelId,
             room.Occupancy,
             room.UbicationFloor,
             room.Price,
             room.Taxes,
             room.Active);

 
    }
}