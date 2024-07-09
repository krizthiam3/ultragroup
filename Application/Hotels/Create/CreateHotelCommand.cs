using ErrorOr;
using MediatR;

namespace Application.Hotels.Create;

public record CreateHotelCommand(
    string Code,
    string Name,
    string Description,
    string PhoneNumber,
    string Country,
    string City,
    string State,
    string ZipCode) : IRequest<ErrorOr<Guid>>;