using RoomsTypes.Common;
using Domain.Rooms;
using ErrorOr;
using MediatR;
using Domain.RoomsTypes;

namespace Application.RoomsTypes.GetAll;


internal sealed class GetAllRoomsTypesQueryHandler : IRequestHandler<GetAllRoomsTypesQuery, ErrorOr<IReadOnlyList<RoomTypesResponse>>>
{
    private readonly IRoomTypesRepository _roomTypesRepository;

    public GetAllRoomsTypesQueryHandler(IRoomTypesRepository roomTypesRepository)
    {
        _roomTypesRepository = roomTypesRepository ?? throw new ArgumentNullException(nameof(roomTypesRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<RoomTypesResponse>>> Handle(GetAllRoomsTypesQuery query, CancellationToken cancellationToken)
    {
        IReadOnlyList<RoomTypes> rooms = await _roomTypesRepository.GetAll();

        return rooms.Select(room => new RoomTypesResponse(
                room.Id.Value,
                room.Code,
                room.Name,
                room.Active
            )).ToList();
    }
}
