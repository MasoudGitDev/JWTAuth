namespace Shared.Auth.Models;

public record LinkModel(string Link,string RouteId = "{email}" ,string Token = "{token}");