using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Customers;

public sealed class Customer : AggregateRoot
{
    public Customer(CustomerId id, string name, string lastName, string documentType, string document, string gender, string email, string phoneNumber, Address address, bool active)
    {
        Id = id;
        Document = document;
        DocumentType = documentType;
        Name = name;
        Gender = gender;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        Active = active;
    }

    private Customer()
    {

    }

    public CustomerId Id { get; private set; }
    public string Document { get; private set;  }
    public string DocumentType { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{Name} {LastName}";
    public string Email { get; private set; } = string.Empty;
    public string Gender { get; private set; }
    public string PhoneNumber { get; private set; }
    public Address Address { get; private set; }
    public bool Active { get; private set; }

    public static Customer UpdateCustomer(Guid id, string name, string lastName, string documentType, string document, string gender, string email, string phoneNumber, Address address, bool active)
    {
        return new Customer(new CustomerId(id), name, document, documentType, lastName, email, gender, phoneNumber, address, active);
    }
}