namespace Hotels.Common;

public record HotelResponse(
Guid Id,
string Code,
string Name,
string Description,
string PhoneNumber,
AddressResponse Address,
bool Active);

public record AddressResponse(
    string Country,
    string City,
    string State,
    string ZipCode);