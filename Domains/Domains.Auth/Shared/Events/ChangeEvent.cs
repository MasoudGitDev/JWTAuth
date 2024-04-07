using Domains.Auth.Shared.Abstractions;
using Domains.Auth.Shared.Enums;

namespace Domains.Auth.Shared.Events;

internal class ChangeEvent<T>(string propertyName, T from, T to) : IDomainEvent
{
    public EventType EventType => EventType.Modified;

    public string? Description
        => $"The <{propertyName}> is changed [from : <{from}>] => [to : <{to}>].";

    public static ChangeEvent<T> Set(string propertyName, T from, T to)
        => new ChangeEvent<T>(propertyName, from, to);
}


