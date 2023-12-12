using Bogus;
using BogusCommon.Models;
using BogusCommon.Models.Options;
using Microsoft.Extensions.Options;

namespace BogusCommon.Services;

public interface IFakeDataGeneratorService
{
    public Customer GetFakeCustomer();
    public IEnumerable<Customer> GetFakeCustomers();
    public Item GetFakeItem();
    public IEnumerable<Item> GetFakeItems();
    public Order GetFakeOrder();
    public IEnumerable<Order> GetFakeOrders();
    public Address GetFakeAddress();
    public IEnumerable<Address> GetFakeAddresses();
}

public class FakeDataGeneratorService : IFakeDataGeneratorService
{
    private readonly Faker<Item> _itemFaker;
    private readonly Faker<Customer> _customerFaker;
    private readonly Faker<Order> _orderFaker;
    private readonly Faker<Address> _addressFaker;

    public FakeDataGeneratorService(IOptions<FakeDataOptions> fakeDataOptions)
    {
        _itemFaker = new Faker<Item>(fakeDataOptions.Value.Locale)
            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
            .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
            .RuleFor(x => x.Unit, f => f.PickRandom<Unit>())
            .RuleFor(x => x.Quantity, (f, x) => x.Unit == Unit.Pieces ? f.Random.Int(1, 100) : Math.Round(f.Random.Decimal(0.2M, 100M),2))
            .RuleFor(x => x.UnitPrice, f => decimal.Parse(f.Commerce.Price(0.2M, 100M)));

        _addressFaker = new Faker<Address>(fakeDataOptions.Value.Locale)
            .RuleFor(x => x.AddressLine1, f => f.Address.StreetAddress())
            .RuleFor(x => x.AddressLine2, f => f.Address.SecondaryAddress())
            .RuleFor(x => x.City, f => f.Address.City())
            .RuleFor(x => x.Country, f => f.Address.Country())
            .RuleFor(x => x.Region, f => f.Address.County())
            .RuleFor(x => x.State, f => f.Address.State())
            .RuleFor(x => x.ZipCode, f => f.Address.ZipCode());

        _customerFaker = new Faker<Customer>(fakeDataOptions.Value.Locale)
            .RuleFor(x => x.FirstName, f => f.Name.FirstName())
            .RuleFor(x => x.LastName, f => f.Name.LastName())
            .RuleFor(x => x.Address, _ => _addressFaker)
            .RuleFor(x => x.Email, (f, x) => f.Internet.Email(x.FirstName, x.LastName))
            .RuleFor(x => x.DateOfBirth, f => DateOnly.FromDateTime(f.Date.PastOffset(25).UtcDateTime))
            .RuleFor(x => x.PhoneNumber, f => f.Person.Phone);

        _orderFaker = new Faker<Order>(fakeDataOptions.Value.Locale)
            .RuleFor(x => x.Number, f => f.Random.Replace("###-###-###"))
            .RuleFor(x => x.Customer, _ => _customerFaker)
            .RuleFor(x => x.Items, _ => _itemFaker.GenerateBetween(1, 5))
            .RuleFor(x => x.OrderDate, f => f.Date.PastOffset(5))
            .RuleFor(x => x.PaymentDate, f => f.Date.RecentOffset());

        if (fakeDataOptions.Value.Seed is null)
        {
            return;
        }
        
        var seed = fakeDataOptions.Value.Seed;
        _itemFaker.UseSeed(seed.Value);
        _customerFaker.UseSeed(seed.Value);
        _addressFaker.UseSeed(seed.Value);
        _orderFaker.UseSeed(seed.Value);
    }

    public Customer GetFakeCustomer()
    {
        var customer = _customerFaker.Generate();
        return customer;
    }

    public IEnumerable<Customer> GetFakeCustomers()
    {
        var customers = _customerFaker.GenerateForever();
        return customers;
    }

    public Item GetFakeItem()
    {
        var item = _itemFaker.Generate();
        return item;
    }

    public IEnumerable<Item> GetFakeItems()
    {
        var items = _itemFaker.GenerateForever();
        return items;
    }

    public Order GetFakeOrder()
    {
        var order = _orderFaker.Generate();
        return order;
    }

    public IEnumerable<Order> GetFakeOrders()
    {
        var orders = _orderFaker.GenerateForever();
        return orders;
    }

    public Address GetFakeAddress()
    {
        var address = _addressFaker.Generate();
        return address;
    }

    public IEnumerable<Address> GetFakeAddresses()
    {
        var addresses = _addressFaker.GenerateForever();
        return addresses;
    }
}