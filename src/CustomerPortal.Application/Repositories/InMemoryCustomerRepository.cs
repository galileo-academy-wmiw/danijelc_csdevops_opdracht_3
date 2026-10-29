using System.Collections.Concurrent;
using CustomerPortal.Domain;

namespace CustomerPortal.Application.Repositories;

public class InMemoryCustomerRepository : ICustomerRepository
{
    private int _nextId = 0;
    private readonly ConcurrentDictionary<int, Customer> _customers = [];

    public int NextId()
    {
        return Interlocked.Increment(ref _nextId);
    }

    public IReadOnlyList<Customer> GetAll()
    {
        var orderedCustomers = _customers.OrderBy(pair => pair.Key).Select(pair => pair.Value);
        return [.. orderedCustomers];
    }

    public Customer? Get(int id)
    {
        _customers.TryGetValue(id, out Customer? customer);
        return customer;
    }

    public bool Save(Customer customer)
    {
        return _customers.TryAdd(customer.Id, customer);
    }
}
