using Rooms.Common;
using ErrorOr;
using MediatR;

namespace Application.Rooms.GetAll;

public record GetAllRoomsQuery() : IRequest<ErrorOr<IReadOnlyList<RoomResponse>>>;