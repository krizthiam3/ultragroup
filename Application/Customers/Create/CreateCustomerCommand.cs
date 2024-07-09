using ErrorOr;
using MediatR;

namespace Application.Customers.Create;

public record CreateCustomerCommand(
    string Document,
    string DocumentType,
    string Name,
    string LastName,
    string Gender,
    string Email,
    string PhoneNumber,
    string Country,
    string City,
    string State,
    string ZipCode) : IRequest<ErrorOr<Guid>>;