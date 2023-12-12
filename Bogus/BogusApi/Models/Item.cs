namespace Bogus.Models;

public class Item
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Unit Unit { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => Math.Round(Quantity * UnitPrice,2);
    
}