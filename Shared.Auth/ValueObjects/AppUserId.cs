using Shared.Auth.Exceptions;

namespace Shared.Auth.ValueObjects;
public class AppUserId :IEquatable<Guid> {
    public Guid Value { get; }

    private AppUserId() => Value = Guid.NewGuid();
    public AppUserId(Guid entityId) {
        if(entityId == Guid.Empty) {
            throw new InvalidGuidException();
        }
        Value = entityId;
    }

    public static AppUserId Create() => new();

    public bool Equals(Guid other) => Value == other;

    public static implicit operator Guid(AppUserId entityId) => entityId.Value;
    public static implicit operator AppUserId(Guid guid) => new(guid);
    public static implicit operator string(AppUserId entityId) => entityId.Value.ToString();

}
