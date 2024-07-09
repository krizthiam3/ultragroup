using ErrorOr;

namespace Domain.DomainErrors
{
    public static partial class Errors
    {
        public static class Hotel
        {
            public static Error AddressWithBadFormat =>
                Error.Validation("Hotel.Address", "Address is not valid.");
        }
    }
}
