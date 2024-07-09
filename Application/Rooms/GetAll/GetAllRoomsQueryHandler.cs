using Rooms.Common;
using Domain.Rooms;
using ErrorOr;
using MediatR;

namespace Application.Rooms.GetAll;


internal sealed class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, ErrorOr<IReadOnlyList<RoomResponse>>>
{
    private readonly IRoomRepository _roomRepository;

    public GetAllRoomsQueryHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<RoomResponse>>> Handle(GetAllRoomsQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<Room> rooms = await _roomRepository.GetAll();

        return rooms.Select(room => new RoomResponse(
                room.Id.Value,
                room.Code,
                room.Name,
                room.TypeId,
                room.HotelId,
                room.Occupancy,
                room.UbicationFloor,
                room.Price,
                room.Taxes,
                room.Active
            )).ToList();
    }
}
