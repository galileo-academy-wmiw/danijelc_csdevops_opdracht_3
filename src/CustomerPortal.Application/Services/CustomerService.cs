using CustomerPortal.Application.Repositories;
using CustomerPortal.Application.Responses;

namespace CustomerPortal.Application.Services;

public class CustomerService(ICustomerRepository repository)
{
    public CustomerResponse? GetCustomer(int id)
    {
        var customer = repository.Get(id);
        if (customer is null)
        {
            return null;
        }

        return new CustomerResponse(
            customer.Surname,
            customer.Forename,
            customer.Discount,
            customer.Address
        );
    }
}
