namespace AN.Domain.Entities.Shared;
public record Currency
{
    private Currency(CurrencyId id, CurrencyName name)
    {
        Id = id;
        Name = name;
    }

    public CurrencyId Id { get; init; }
    public CurrencyName Name { get; private set; }
    
    public static Currency Create(CurrencyId id, CurrencyName name)
    {
        return new Currency(id, name);
    }

    public void Update(CurrencyName name)
    {
        Name = name;
    }
}