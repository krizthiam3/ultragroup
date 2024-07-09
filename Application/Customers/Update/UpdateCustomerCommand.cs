namespace Application.Customers.Update;
using ErrorOr;
using MediatR;

public record UpdateCustomerCommand(
    Guid Id,
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
    string ZipCode,
    bool Active) : IRequest<ErrorOr<Unit>>;