namespace Customers.Common;

public record CustomerResponse(
Guid Id,
string DocumentType,
string Document,
string Name,
string LastName,
string Email,
string Gender,
string PhoneNumber,
AddressResponse Address,
bool Active);

public record AddressResponse(
    string Country,
    string City,
    string State,
    string ZipCode);