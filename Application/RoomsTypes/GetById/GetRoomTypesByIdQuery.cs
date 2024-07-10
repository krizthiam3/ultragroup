using RoomsTypes.Common;
using ErrorOr;
using MediatR;

namespace Application.RoomsTypes.GetById;

public record GetRoomTypesByIdQuery(Guid Id) : IRequest<ErrorOr<RoomTypesResponse>>;