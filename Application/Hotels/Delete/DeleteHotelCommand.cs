namespace Application.Hotels.Delete;
using ErrorOr;
using MediatR;

public record DeleteHotelCommand(Guid Id) : IRequest<ErrorOr<Unit>>;