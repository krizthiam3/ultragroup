using Hotels.Common;
using ErrorOr;
using MediatR;

namespace Application.Hotels.GetAll;

public record GetAllHotelsQuery() : IRequest<ErrorOr<IReadOnlyList<HotelResponse>>>;