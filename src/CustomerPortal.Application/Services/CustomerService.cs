using CustomerPortal.Application.Repositories;
using CustomerPortal.Application.Requests;
using CustomerPortal.Application.Responses;
using CustomerPortal.Application.Validators;
using CustomerPortal.Domain;

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
            customer.Id,
            customer.Surname,
            customer.Forename,
            customer.Discount,
            customer.Address
        );
    }

    public CustomerResponse? CreateCustomer(CreateCustomerRequest request)
    {
        // validate request
        var createCustomerRequestValidator = new CreateCustomerRequestValidator();
        var result = createCustomerRequestValidator.Validate(request);

        if (!result.IsValid)
        {
            return null;
        }

        // save new customer
        var customer = repository.Save(request);
        return new CustomerResponse(
            customer.Id,
            customer.Surname,
            customer.Forename,
            customer.Discount,
            customer.Address
        );
    }
}
