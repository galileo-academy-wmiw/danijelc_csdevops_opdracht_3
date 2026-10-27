using CustomerPortal.Domain;

namespace CustomerPortal.Application.Repositories;

public class InMemoryCustomerRepository : ICustomerRepository
{
    private readonly List<Customer> customers = [];

    public IReadOnlyList<Customer> GetAll()
    {
        return customers.AsReadOnly();
    }

    public Customer? Get(int id)
    {
        if (id < 1)
        {
            return null;
        }
        return customers.FirstOrDefault(customer => customer.Id == id);
    }

    public void Save(Customer customer)
    {
        customers.Add(customer);
    }
}
