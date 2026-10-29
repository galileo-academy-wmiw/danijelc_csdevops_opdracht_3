using CustomerPortal.Domain;

namespace CustomerPortal.Application.Responses;

public class CustomerData(Customer customer)
{
    public int Id { get; } = customer.Id;
    public string Surname { get; } = customer.Surname;
    public string Forename { get; } = customer.Forename;
    public decimal Discount { get; } = customer.Discount;
    public string Address { get; } = customer.Address;
}
