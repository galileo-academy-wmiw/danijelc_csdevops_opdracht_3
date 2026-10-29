using CustomerPortal.Domain;

namespace CustomerPortal.Application.Responses;

public class CustomerData(Customer customer)
{
    public int Id { get; } = customer.Id;
    public string Surname { get; } = customer.Surname;
    public string Forename { get; } = customer.Forename;
    public string Address { get; } = customer.Address;
}

public enum Status
{
    Success,
    ValidationFailed,
    NotFound,
    AlreadyExists,
}

public record Result<T>(Status Status, T Value, Error? Error);
