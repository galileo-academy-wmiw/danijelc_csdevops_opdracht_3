namespace CustomerPortal.Application.Responses;

public record Result<T>(Status Status, T? Value, Error? Error);
