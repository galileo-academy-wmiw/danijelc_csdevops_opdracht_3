namespace CustomerPortal.Application.Responses;

public record CustomerBody(
    int Id,
    string Surname,
    string Forename,
    decimal Discount,
    string Address
);

public record CustomerResponse(int Code, CustomerBody? Body, Error? Error);
