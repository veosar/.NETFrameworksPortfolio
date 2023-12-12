namespace Bogus.Models;

public class Order
{
    public string Number { get; set; }
    public Customer Customer { get; set; }
    public List<Item> Items { get; set; }
    public decimal TotalAmount => Items.Sum(x => x.TotalPrice);
    public DateTime OrderDate { get; set; }
    public DateTime PaymentDate { get; set; }
}