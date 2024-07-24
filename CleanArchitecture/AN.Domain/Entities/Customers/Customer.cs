using System.Diagnostics.CodeAnalysis;
using AN.Domain.Entities.Shared;

namespace AN.Domain.Entities.Customers;

public class Customer
{
    [ExcludeFromCodeCoverage]
    private Customer()
    {
        
    }
    private Customer(CustomerId id, Email email, FirstName firstName, LastName lastName)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }
    public CustomerId Id { get; private set; }
    public Email Email { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }

    public static Customer Create(CustomerId id, Email email, FirstName firstName, LastName lastName)
    {
        return new Customer(id, email, firstName, lastName);
    }

    public void Update(Email email, FirstName firstName, LastName lastName)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }
}