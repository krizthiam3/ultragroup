using Hotels.Common;
using ErrorOr;
using MediatR;

namespace Application.Hotels.GetById;

public record GetHotelByIdQuery(Guid Id) : IRequest<ErrorOr<HotelResponse>>;