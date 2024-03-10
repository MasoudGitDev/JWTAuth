using Shared.Auth.Exceptions;

namespace Domains.Auth.ValueObjects;
internal class EntityId {
    public Guid Value { get; }

    private EntityId() => Value = Guid.NewGuid();
    public EntityId(Guid entityId) {
        if(entityId == Guid.Empty) {
            throw new InvalidGuidException();
        }
        Value = entityId;
    }

    public static EntityId Create() => new();
    public static implicit operator Guid(EntityId entityId) => entityId.Value;
    public static implicit operator EntityId(Guid guid) => new(guid);

}
