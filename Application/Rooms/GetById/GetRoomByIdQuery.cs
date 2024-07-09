using Rooms.Common;
using ErrorOr;
using MediatR;

namespace Application.Rooms.GetById;

public record GetRoomByIdQuery(Guid Id) : IRequest<ErrorOr<RoomResponse>>;