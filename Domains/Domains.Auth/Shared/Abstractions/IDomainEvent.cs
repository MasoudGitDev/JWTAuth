using Domains.Auth.Shared.Enums;

namespace Domains.Auth.Shared.Abstractions;
public interface IDomainEvent {
    EventType EventType { get; }
    string? Description { get; }
}
