namespace CustomerPortal.Application.Responses;


public record CustomerResponse(int Id, string Surname, string Forename, decimal Discount, string Address);
