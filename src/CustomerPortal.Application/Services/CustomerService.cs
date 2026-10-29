using CustomerPortal.Application.Repositories;
using CustomerPortal.Application.Requests;
using CustomerPortal.Application.Responses;
using CustomerPortal.Application.Validators;
using CustomerPortal.Domain;

namespace CustomerPortal.Application.Services;

public class CustomerService(ICustomerRepository repository)
{
    public Result<IReadOnlyList<CustomerData>> GetAll()
    {
        var customers = repository.GetAll();

        List<CustomerData> customersData = [];

        foreach (var customer in customers)
        {
            customersData.Add(new CustomerData(customer));
        }
        return new Result<IReadOnlyList<CustomerData>>(Status.Success, customersData, null);
    }

    public Result<CustomerData?> GetCustomer(int id)
    {
        var customer = repository.Get(id);
        if (customer is null)
        {
            return new Result<CustomerData?>(Status.NotFound, null, null);
        }

        return new Result<CustomerData?>(Status.Success, new CustomerData(customer), null);
    }

    public Result<CustomerData?> CreateCustomer(CreateCustomerRequest request)
    {
        // validate request
        var createCustomerRequestValidator = new CreateCustomerRequestValidator();
        var result = createCustomerRequestValidator.Validate(request);

        if (!result.IsValid)
        {
            return new Result<CustomerData?>(
                Status.ValidationFailed,
                null,
                new Error(result.ToString())
            );
        }

        // save new customer
        var customer = new Customer(
            repository.NextId(),
            request.Surname,
            request.Forename,
            request.Address
        );

        var success = repository.Save(customer);
        if (!success)
        {
            return new Result<CustomerData?>(
                Status.AlreadyExists,
                null,
                new Error($"Customer with id {customer.Id} already exists")
            );
        }

        return new Result<CustomerData?>(Status.Success, new CustomerData(customer), null);
    }
}
