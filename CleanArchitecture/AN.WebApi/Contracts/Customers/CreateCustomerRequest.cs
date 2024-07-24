namespace AN.WebApi.Contracts.Customers;

public record CreateCustomerRequest(string Email, string FirstName, string LastName);