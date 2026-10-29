namespace CustomerPortal.Application.Responses;

public record CustomerData(
    int Id,
    string Surname,
    string Forename,
    decimal Discount,
    string Address
);

public enum Status
{
    Success,
    ValidationFailed,
    NotFound,
    AlreadyExists,
}

public record CustomerResponse(Status Status, List<CustomerData> Customers, Error? Error);
