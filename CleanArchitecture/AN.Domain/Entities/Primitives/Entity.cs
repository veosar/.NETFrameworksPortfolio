namespace AN.Domain.Entities.Primitives;

public abstract class Entity
{
    private readonly List<DomainEvent> _domainEvents = [];
    public IReadOnlyCollection<DomainEvent> GetDomainEvents() => _domainEvents.ToList();
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}