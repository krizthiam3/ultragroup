using Rooms.Common;
using ErrorOr;
using MediatR;

namespace Application.Rooms.GetFilter;

public record GetFilterRoomsQuery(string City, int Occupancy, bool Status) : IRequest<ErrorOr<IReadOnlyList<RoomResponse>>>;
