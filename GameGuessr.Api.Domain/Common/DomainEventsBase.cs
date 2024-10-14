using MediatR;

namespace GameGuessr.Api.Domain.Common;

public abstract class DomainEventsBase : IHasDomainEvents
{
    private readonly List<INotification> _domainEvents = new();
    public IReadOnlyList<INotification> DomainEvents => _domainEvents;

    public void ClearDomainEvents() =>
        _domainEvents.Clear();

    protected void RaiseDomainEvent(INotification domainEvent) =>
        _domainEvents.Add(domainEvent);
}
