using Domains.Auth.Shared.ValueObjects;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Auth.Shared.Abstractions;

internal abstract class AppUserAggregateRoot : IdentityUser<Guid> {

    [NotMapped]
    public EntityId RootId {
        get => Id;
        set => Id = value;
    }

    /// <summary>
    /// use for event sourcing or concurrency control.
    /// </summary>
    public uint Version { get; private set; } = 0;


    private readonly LinkedList<IDomainEvent> _domainEvents = new();

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;
    public virtual void Apply(IDomainEvent domainEvent) { }

    public void ClearEvents() => _domainEvents.Clear();
    public void RemoveEvent(IDomainEvent domainEvent) => _domainEvents.Remove(domainEvent);
    public void RaiseEvent(IDomainEvent domainEvent) {
        try {
            Apply(domainEvent);
            _domainEvents.AddLast(domainEvent);
            checked {
                Version++;
            }
        }
        catch(OverflowException) {
            Version = 0;
        }
    }
}
