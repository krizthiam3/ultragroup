using Rooms.Common;
using ErrorOr;
using MediatR;

namespace Application.Rooms.GetFilter;

public record GetFilterRoomsQuery(string City, int Occupancy, DateTime StartDate, DateTime EndDate,  bool Status) : IRequest<ErrorOr<IReadOnlyList<RoomResponse>>>;
