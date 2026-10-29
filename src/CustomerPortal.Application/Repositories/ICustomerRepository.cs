using CustomerPortal.Domain;

namespace CustomerPortal.Application.Repositories;

public interface ICustomerRepository
{
    IReadOnlyList<Customer> GetAll();
    Customer? Get(int id);
    bool Save(Customer customer);
    int NextId();
}
