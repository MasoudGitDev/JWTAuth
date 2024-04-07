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
    public AppUserId(string id) {
        if(String.IsNullOrWhiteSpace(id)) {
            throw new InvalidGuidException();
        }
        Guid.TryParse(id, out var guid);
        if(guid == Guid.Empty) {
            throw new InvalidGuidException();
        }
        Value = guid;
    }

    public static AppUserId Create() => new();
    public static AppUserId Create(string id) => new(id);

    public bool Equals(Guid other) => Value == other;

    public static implicit operator Guid(AppUserId entityId) => entityId.Value;
    public static implicit operator AppUserId(Guid guid) => new(guid);
    public static implicit operator string(AppUserId entityId) => entityId.Value.ToString();
    public static implicit operator AppUserId(string id) => new(id);

}
