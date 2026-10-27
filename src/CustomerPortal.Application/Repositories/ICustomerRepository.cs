using CustomerPortal.Application.Requests;
using CustomerPortal.Domain;

namespace CustomerPortal.Application.Repositories;

public interface ICustomerRepository
{
    Customer? Get(int id);
    Customer Save(CreateCustomerRequest newCustomer);
}
