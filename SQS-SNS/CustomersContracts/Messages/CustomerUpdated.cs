namespace CustomersContracts.Messages;

public class CustomerUpdated : ISqsMessage
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public decimal OldTotalMoneySpent { get; set; }
    public decimal NewTotalMoneySpent { get; set; }
}