namespace AN.Application.Customers.Get;

public record CustomerResponse(Guid Id, string Email, string FirstName, string LastName);