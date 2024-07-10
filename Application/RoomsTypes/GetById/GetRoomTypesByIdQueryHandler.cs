using RoomsTypes.Common;
using Domain.RoomsTypes;
using ErrorOr;
using MediatR;

namespace Application.RoomsTypes.GetById;


internal sealed class GetRoomTypesByIdQueryHandler : IRequestHandler<GetRoomTypesByIdQuery, ErrorOr<RoomTypesResponse>>
{
    private readonly IRoomTypesRepository _roomTypesRepository;

    public GetRoomTypesByIdQueryHandler(IRoomTypesRepository roomRepository)
    {
        _roomTypesRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
    }

    public async Task<ErrorOr<RoomTypesResponse>> Handle(GetRoomTypesByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _roomTypesRepository.GetByIdAsync(new RoomTypesId(query.Id)) is not RoomTypes roomTypes)
        {
            return Error.NotFound("RoomTypes.NotFound", "The roomTypes with the provide Id was not found.");
        }

        return new RoomTypesResponse(
        roomTypes.Id.Value,
        roomTypes.Code,
        roomTypes.Name,
        roomTypes.Active);

 
    }
}