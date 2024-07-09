namespace Application.Hotels.Update;
using ErrorOr;
using MediatR;

public record UpdateHotelCommand(
    Guid Id,
    string Code,
    string Name,
    string Description,
    string PhoneNumber,
    string Country,
    string City,
    string State,
    string ZipCode,
    bool Active) : IRequest<ErrorOr<Unit>>;