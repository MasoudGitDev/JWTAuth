using Apps.Services.Implementations.Jwt;
using FluentAssertions;
using Shared.Auth.Constants;
using Shared.Auth.Enums;
using Shared.Auth.Models;

namespace UTests.Apps.Auth.Services;
public class JwtServiceTests {



    private readonly AuthTokenSettingsModel _authSetting =
            new(
                "TNIvdpRbRIZHJU2NzG9tMnJKV3ZzZz09TNIvdpRbRIZHJU2NzG9tMnJKV3ZzZz09",
                "https://localhost:7224",
                "https://localhost:7224",
                60);


    private readonly  JwtService _jwtService;

    public JwtServiceTests() {
        _jwtService = JwtService.Create(_authSetting);
    }

    [Fact]
    public async Task GenerateToken_Should_Return_Valid_AccountResult() {

        //Arrange 
        Dictionary<string,string> claims = new(){
            { TokenKey.DisplayName , "TestUser" } ,
            { TokenKey.Id , Guid.NewGuid().ToString() } ,
            { TokenKey.Issuer , _authSetting.Issuer.ToString() } ,
            { TokenKey.IssuerAt , DateTime.UtcNow.ToString() } ,
            { TokenKey.Audience , _authSetting.Audience } ,
            { TokenKey.ExpireAt , DateTime.UtcNow.AddMinutes(_authSetting.ExpireMinutes).ToString() } ,
            { TokenKey.UserId , Guid.NewGuid().ToString() } ,
            { TokenKey.NotBefore , DateTime.UtcNow.ToString() },
            { TokenKey.IsBlocked , false.ToString() } ,
            { TokenKey.Reason , "OK"  }
        };

        //Act
        var accountResult = await _jwtService.GenerateAsync(claims);

        //Assert
        SharedAssert(accountResult);
        accountResult.KeyValueClaims[TokenKey.IsBlocked].Should().Be("False");
        accountResult.KeyValueClaims[TokenKey.Reason].Should().Be("OK");
    }

    [Fact]
    public async Task EvaluateToken_should_Return_Valid_AccountResult() {
        //Arrange
        string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE3MTM5Mzk3NjUsImF1ZCI6WyJodHRwczovL2xvY2FsaG9zdDo3MjI0IiwiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIyNCJdLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjI0IiwiZXhwIjoxNzEzOTQzMzY1LCJpZCI6IjNmYjE5ZmRlLTI5NGUtNGY3Zi1iMjhlLTEzZDA5YWVkMTIyMyIsIlVzZXJJZCI6ImYwNTA4ZDRjLTQzMDMtNGRkYS04NTRmLTBmMGU1NjJiMTk2MyIsIlJlYXNvbiI6Ik5vdENvbmZpcm1lZEVtYWlsIiwiSXNCbG9ja2VkIjoiVHJ1ZSIsIkRpc3BsYXlOYW1lIjoiTWFzb3VkMSIsIm5iZiI6MTcxMzkzOTc2NX0.2gAolLah0vptXcMqpE0BNDiW5Yjw-emNojkG-hj24rY";

        //Act
        var accountResult = await _jwtService.EvaluateAsync(token , async (id) => await Task.FromResult(id));

        //Assert
        SharedAssert(accountResult);
    }

    private void SharedAssert(AccountResult result) {
        result.Should().NotBeNull();
        result.ResultStatus.Should().Be(ResultStatus.Succeed);
        result.AuthToken.Should().NotBeNullOrEmpty();
        result.KeyValueClaims.Should().NotBeNull().And.NotBeEmpty();

        var claims = result.KeyValueClaims;

        claims.Should().BeOfType<Dictionary<string , string>>();
        claims.Keys.Should().ContainMatch(
            TokenKey.Id ,
            TokenKey.Issuer ,
            TokenKey.IssuerAt ,
            TokenKey.Audience ,
            TokenKey.ExpireAt ,
            TokenKey.UserId ,
            TokenKey.IsBlocked ,
            TokenKey.Reason ,
            TokenKey.DisplayName ,
            TokenKey.NotBefore).And.HaveCount(10);

        claims.Where(x => x.Key == TokenKey.Issuer).FirstOrDefault().Value
            .Should().Be(_authSetting.Issuer);
        claims.Where(x => x.Key == TokenKey.Audience).FirstOrDefault().Value
            .Should().Be(_authSetting.Audience);
    }

}
