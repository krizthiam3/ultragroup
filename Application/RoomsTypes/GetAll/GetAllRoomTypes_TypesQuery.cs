using RoomsTypes.Common;
using ErrorOr;
using MediatR;

namespace Application.RoomsTypes.GetAll;

public record GetAllRoomsTypesQuery() : IRequest<ErrorOr<IReadOnlyList<RoomTypesResponse>>>;