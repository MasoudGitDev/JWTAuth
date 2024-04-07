using Domains.Auth.Shared.Abstractions;
using Domains.Auth.Shared.Events;
using System.Text.Json.Serialization;

namespace Domains.Auth.AppUserEntity.ValueObjects;
public record class LockInfo {
    public bool IsLocked { get; private set; } = false;
    public DateTime? StartAt { get; private set; } = null;
    public DateTime? EndAt { get; private set; } = null;

    public LockInfo() {

    }
    public static LockInfo Create(bool isLocked = true , DateTime? startAt = null , DateTime? endAt = null) {
        return new LockInfo() {
            IsLocked = isLocked ,
            StartAt = startAt ?? DateTime.UtcNow ,
            EndAt = endAt
        };
    }
    public void Update(bool isLocked , DateTime? startAt , DateTime? endAt) {
        IsLocked = isLocked;
        StartAt = startAt;
        EndAt = endAt;
    }

    public void SetLock(bool isLocked , Action<IDomainEvent> eventAction) {
        var temp = IsLocked;
        IsLocked = isLocked;
        eventAction.Invoke(ChangeEvent<bool>.Set(nameof(IsLocked) , temp , isLocked));
    }

    public void SetStartAt(DateTime? startAt , Action<IDomainEvent> eventAction) {
        var temp = StartAt;
        StartAt = startAt;
        eventAction.Invoke(ChangeEvent<DateTime?>.Set(nameof(StartAt) , temp , startAt));
    }

    public void SetEndAt(DateTime? endAt , Action<IDomainEvent> eventAction) {
        var temp = EndAt;
        EndAt = endAt;
        eventAction.Invoke(ChangeEvent<DateTime?>.Set(nameof(EndAt) , temp , endAt));
    }

}
