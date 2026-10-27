using CustomerPortal.Application.Repositories;
using CustomerPortal.Application.Requests;
using CustomerPortal.Application.Responses;
using CustomerPortal.Application.Validators;
using CustomerPortal.Domain;

namespace CustomerPortal.Application.Services;

public class CustomerService(ICustomerRepository repository)
{
    private static int nextId = 1;

    public CustomerResponse GetAll()
    {
        var customers = repository.GetAll();

        List<CustomerData> customersData = [];

        foreach (var customer in customers)
        {
            customersData.Add(
                new CustomerData(
                    customer.Id,
                    customer.Surname,
                    customer.Forename,
                    customer.Discount,
                    customer.Address
                )
            );
        }
        return new CustomerResponse(Status.Success, customersData, null);
    }

    public CustomerResponse GetCustomer(int id)
    {
        var customer = repository.Get(id);
        if (customer is null)
        {
            return new CustomerResponse(Status.NotFound, [], null);
        }

        return new CustomerResponse(
            Status.Success,
            [
                new CustomerData(
                    customer.Id,
                    customer.Surname,
                    customer.Forename,
                    customer.Discount,
                    customer.Address
                ),
            ],
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
            return new CustomerResponse(Status.ValidationFailed, [], new Error(result.ToString()));
        }

        // save new customer
        var customer = new Customer(nextId++, request.Surname, request.Forename, request.Address);
        repository.Save(customer);
        return new CustomerResponse(
            Status.Success,
            [
                new CustomerData(
                    customer.Id,
                    customer.Surname,
                    customer.Forename,
                    customer.Discount,
                    customer.Address
                ),
            ],
            null
        );
    }
}
