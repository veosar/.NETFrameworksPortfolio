namespace AN.WebApi.Contracts.Customers;

public record UpdateCustomerRequest(string Email, string FirstName, string LastName);