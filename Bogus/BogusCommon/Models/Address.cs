namespace BogusCommon.Models;

public class Address
{
    public int Id { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
    public string Region { get; set; }
    public string State { get; set; }
}