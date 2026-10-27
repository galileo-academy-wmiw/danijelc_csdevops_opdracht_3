using CustomerPortal.Application.Repositories;
using CustomerPortal.Application.Requests;
using CustomerPortal.Application.Responses;
using CustomerPortal.Application.Validators;

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
            201,
            new CustomerBody(
                customer.Id,
                customer.Surname,
                customer.Forename,
                customer.Discount,
                customer.Address
            ),
            null
        );
    }

    public CustomerResponse CreateCustomer(CreateCustomerRequest request)
    {
        // validate request
        var createCustomerRequestValidator = new CreateCustomerRequestValidator();
        var result = createCustomerRequestValidator.Validate(request);

        if (!result.IsValid)
        {
            return new CustomerResponse(400, null, new Error(result.ToString()));
        }

        // save new customer
        var customer = repository.Save(request);
        return new CustomerResponse(
            200,
            new CustomerBody(
                customer.Id,
                customer.Surname,
                customer.Forename,
                customer.Discount,
                customer.Address
            ),
            null
        );
    }
}
