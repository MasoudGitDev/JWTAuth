//using Apps.Auth.Services;
//using FluentAssertions;
//using Shared.Auth.Enums;
//using Shared.Auth.Models;
//using Shared.Auth.ValueObjects;

//namespace UTests.Apps.Auth.Services;
//public class JwtServiceTests {

//    private readonly AuthTokenSettingsModel _authSetting =
//        new(
//            "TNIvdpRbRIZHJU2NzG9tMnJKV3ZzZz09TNIvdpRbRIZHJU2NzG9tMnJKV3ZzZz09",
//            "https://localhost:7224",
//            "https://localhost:7224",
//            60);


//    private readonly  JwtService _jwtService;

//    public JwtServiceTests() {
//        _jwtService = JwtService.Create(_authSetting);
//    }

//    //[Fact]
//    //public async Task GenerateTokenAsync_Should_GenerateToken_Successfully() {

//    //    //Arrange
//    //    AppUserId appUserId = AppUserId.Create();

//    //    //Act
//    //    var result =  await _jwtService.GenerateTokenAsync(appUserId);

//    //    //Assert
//    //    SharedAssert(result);

//    //}

//    //[Fact]
//    //public async Task EvaluateAsync_Should_EvaluateToken_Successfully() {

//    //    //Arrange
//    //    string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjI1YWQ4MGEwLWM3ZjEtNGQ2Yy1iYzMyLWI3MzRhN2YxMGI0ZiIsIlVzZXJJZCI6IjUzY2E5N2M3LTU4MzYtNDhlMS05MDc2LWY0NTM2NTVhZjkzYiIsImlhdCI6IjMvMjcvMjAyNCA4OjI4OjE5IEFNIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzIyNCIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjcyMjQiLCJleHAiOiIzLzI3LzIwMjQgOToyODoxOSBBTSIsIklzQmxvY2tlZCI6IkZhbHNlIn0.diljI9ZPV0mt88kSU1uLIwgVHDXl1LrkC8QE1rY-fpA";

//    //    //Act
//    //    var result =  await _jwtService.EvaluateAsync(token);

//    //    //Assert
//    //    SharedAssert(result);

//    //}

//    //// private methods

//    private void SharedAssert(AccountResult result) {
//        result.Should().NotBeNull();
//        result.ResultStatus.Should().Be(ResultStatus.Succeed);
//        result.AuthToken.Should().NotBeNullOrEmpty();
//        result.KeyValueClaims.Should().NotBeNull().And.NotBeEmpty();

//        var claims = result.KeyValueClaims;

//        claims.Should().BeOfType<Dictionary<string , string>>();
//        claims.Keys.Should().ContainMatch(
//            AuthTokenType.Id ,
//            AuthTokenType.Issuer ,
//            AuthTokenType.IssuerAt ,
//            AuthTokenType.Audience ,
//            AuthTokenType.ExpireAt ,
//            AuthTokenType.UserId ,
//            AuthTokenType.IsBlocked);

//        claims.Where(x => x.Key == AuthTokenType.Issuer).FirstOrDefault().Value
//            .Should().Be(_authSetting.Issuer);
//        claims.Where(x => x.Key == AuthTokenType.Audience).FirstOrDefault().Value
//            .Should().Be(_authSetting.Audience);
//    }

//}
