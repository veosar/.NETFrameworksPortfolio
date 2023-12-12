namespace BogusCommon.Models;

public class Order
{
    public int Id { get; set; }
    public string Number { get; set; }
    public Customer Customer { get; set; }
    public List<Item> Items { get; set; }
    public decimal TotalAmount => Items.Sum(x => x.TotalPrice);
    public DateTimeOffset OrderDate { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
}