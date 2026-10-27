using CustomerPortal.Domain;

namespace CustomerPortal.Application.Repositories;

public interface ICustomerRepository
{
    IReadOnlyList<Customer> GetAll();
    Customer? Get(int id);
    void Save(Customer customer);
}
