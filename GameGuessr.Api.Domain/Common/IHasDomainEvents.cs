using System.Collections.Generic;
using MediatR;

public interface IHasDomainEvents
{
    IReadOnlyList<INotification> DomainEvents { get; }

    public void ClearDomainEvents();
}