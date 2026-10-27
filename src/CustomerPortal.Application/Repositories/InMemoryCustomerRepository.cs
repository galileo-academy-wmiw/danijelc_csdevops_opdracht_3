using CustomerPortal.Application.Requests;
using CustomerPortal.Domain;

namespace CustomerPortal.Application.Repositories;

public class InMemoryCustomerRepository : ICustomerRepository
{
    private readonly List<Customer> customers = [];

    public Customer? Get(int id)
    {
        if (id < 1)
        {
            return null;
        }
        return customers.FirstOrDefault(customer => customer.Id == id);
    }

    public Customer Save(CreateCustomerRequest newCustomer)
    {
        var customer = new Customer(newCustomer.Surname, newCustomer.Forename, newCustomer.Address);
        customers.Add(customer);
        return customer;
    }
}
