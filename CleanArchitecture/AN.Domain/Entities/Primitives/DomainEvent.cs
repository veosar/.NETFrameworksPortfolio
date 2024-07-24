using MediatR;

namespace AN.Domain.Entities.Primitives;

public record DomainEvent(Guid Id) : INotification;
