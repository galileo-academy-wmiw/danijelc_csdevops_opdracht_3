using CustomerPortal.Domain;

namespace CustomerPortal.Application.Responses;

public record CustomerData(Customer Customer)
{
    public int Id = Customer.Id;
    public string Surname = Customer.Surname;
    public string Forename = Customer.Forename;
    public decimal Discount = Customer.Discount;
}

public enum Status
{
    Success,
    ValidationFailed,
    NotFound,
    AlreadyExists,
}

public record Result<T>(Status Status, T Value, Error? Error);
