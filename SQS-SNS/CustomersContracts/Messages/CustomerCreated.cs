namespace CustomersContracts.Messages;

public class CustomerCreated : ISqsMessage
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public decimal TotalMoneySpent { get; set; }
}