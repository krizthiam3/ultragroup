using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public partial record Address
    {
        public Address(string country, string city, string state, string zipCode)
        {
            Country = country;           
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public string Country { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string ZipCode { get; init; }

        public static Address? Create(string country, string city, string state, string zipCode)
        {
            if (string.IsNullOrEmpty(country) ||  string.IsNullOrEmpty(city) || string.IsNullOrEmpty(state) || string.IsNullOrEmpty(zipCode))
            {
                return null;
            }

            return new Address(country,  city, state, zipCode);
        }
    }
}
