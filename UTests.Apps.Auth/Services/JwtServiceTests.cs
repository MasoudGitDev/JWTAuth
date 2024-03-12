using Apps.Auth.Services;
using FluentAssertions;
using Shared.Auth.Enums;
using Shared.Auth.Models;
using Shared.Auth.ValueObjects;
using System.Security.Claims;

namespace UTests.Apps.Auth.Services;
public class JwtServiceTests {

    private readonly AuthTokenSettingsModel _authSetting = 
        new AuthTokenSettingsModel(
            "TNIvdpRbRIZHJU2NzG9tMnJKV3ZzZz09TNIvdpRbRIZHJU2NzG9tMnJKV3ZzZz10",
            "https://localhost:7224",
            "https://localhost:7224",
            60);


    private readonly  JwtService _jwtService;

    public JwtServiceTests()
    {
        _jwtService = JwtService.Create(_authSetting);
    }

    [Fact]
    public async Task GenerateTokenAsync_Should_GenerateToken_Successfully() {

        //Arrange
        AppUserId appUserId = AppUserId.Create();

        //Act
        var result =  await _jwtService.GenerateTokenAsync(appUserId);

        //Assert
        SharedAssert(result);

    }

    [Fact]
    public async Task EvaluateAsync_Should_EvaluateToken_Successfully() {

        //Arrange
        string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjdhMTk1YmYzLTU2YjAtNDQzYy1hMDRmLWU5Y2RjNDU4MmE2YiIsIlVzZXJJZCI6Ijc0YmEwNGY4LTZiNGUtNGNkNS04YjdmLWYzZWUxNzI0YmExNSIsImlhdCI6IjMvMTIvMjAyNCA3OjQ5OjQxIFBNIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIyNCIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcyMjQiLCJleHAiOiIzLzEyLzIwMjQgODo0OTo0MSBQTSJ9.d-fYc9bSVStHekhlq6YGyZJy6lYm6EL57kc68UY36tM";

        //Act
        var result =  await _jwtService.EvaluateAsync(token);

        //Assert
        SharedAssert(result);

    }

    // private methods

    private void SharedAssert(AccountResult result) {
        result.Should().NotBeNull();
        result.ResultStatus.Should().Be(ResultStatus.Succeed);
        result.AuthToken.Should().NotBeNullOrEmpty();
        result.KeyValueClaims.Should().NotBeNull().And.NotBeEmpty();

        var claims = result.KeyValueClaims;

        claims.Should().BeOfType<Dictionary<string,string>>();
        claims.Keys.Should().ContainMatch(
            AuthTokenType.Issuer ,
            AuthTokenType.Audience,
            AuthTokenType.IssuerAt,
            AuthTokenType.Id,
            AuthTokenType.Expire ,
            AuthTokenType.UserId).And.HaveCount(6);

        claims.Where(x => x.Key == AuthTokenType.Issuer).FirstOrDefault().Value
            .Should().Be(_authSetting.Issuer);
        claims.Where(x => x.Key == AuthTokenType.Audience).FirstOrDefault().Value
            .Should().Be(_authSetting.Audience);
    }

}
