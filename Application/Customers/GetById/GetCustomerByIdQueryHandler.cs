using Customers.Common;
using Domain.Customers;
using ErrorOr;
using MediatR;

namespace Application.Customers.GetById;


internal sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, ErrorOr<CustomerResponse>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<ErrorOr<CustomerResponse>> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
    {
        if (await _customerRepository.GetByIdAsync(new CustomerId(query.Id)) is not Customer customer)
        {
            return Error.NotFound("Customer.NotFound", "The customer with the provide Id was not found.");
        }

        return new CustomerResponse(
            customer.Id.Value, 
            customer.Document,
            customer.DocumentType,
            customer.Name, 
            customer.LastName, 
            customer.Email, 
            customer.Gender,
            customer.PhoneNumber, 
            new AddressResponse(customer.Address.Country,
            customer.Address.City,
            customer.Address.State,
            customer.Address.ZipCode),
            customer.Active);
    }
}


