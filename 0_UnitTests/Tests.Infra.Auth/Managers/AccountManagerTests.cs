using Apps.Services.Services.Security;
using Domains.Auth.AppUserEntity.Aggregate;
using FluentAssertions;
using Infra.Auth.Implements.Managers;
using Microsoft.AspNetCore.Identity;
using Moq;
using Moq.AutoMock;
using Shared.Auth.Constants;
using Shared.Auth.Models;
using Shared.Auth.ValueObjects;

namespace Tests.Infra.Auth.Managers;
public class AccountManagerTests {

    private readonly AutoMocker _mocker;
    private readonly AccountManager _accountManager;

    public AccountManagerTests() {
        _mocker = new AutoMocker();
        _accountManager = _mocker.CreateInstance<AccountManager>();
    }


    [Fact]
    public async Task Login_With_UserName_Should_Return_Valid_AccountResult() {
        //Arrange
        var (mockUser, userClaims) = CreateUserAndClaims();
        _mocker.GetMock<UserManager<AppUser>>()
           .Setup(x => x.FindByNameAsync(mockUser.UserName))
           .ReturnsAsync(mockUser);

        // Shared (Arrange,Act,Assert)
        await Login_Shared(mockUser.UserName , mockUser , userClaims);

        // Assert
        _mocker.Verify<UserManager<AppUser>>(x => x.FindByNameAsync(mockUser.UserName) , Times.Once);

    }

    [Fact]
    public async Task Login_With_Email_Should_Return_Valid_AccountResult() {
        //Arrange
        var (mockUser, userClaims) = CreateUserAndClaims();
        _mocker.GetMock<UserManager<AppUser>>()
           .Setup(x => x.FindByEmailAsync(mockUser.Email))
           .ReturnsAsync(mockUser);

        // Shared (Arrange,Act,Assert)
        await Login_Shared(mockUser.Email , mockUser , userClaims);
        
        // Assert
        _mocker.Verify<UserManager<AppUser>>(x => x.FindByEmailAsync(mockUser.Email) , Times.Once);      

    }


    //=============================== privates
    private static (AppUser MockUser, Dictionary<string , string> Claims) CreateUserAndClaims() {

        var mockUser = new AppUser {
            Id = Guid.NewGuid() ,
            UserName = "AppUser1" ,
            Email= "AppUser1@gmail.com" ,
            EmailConfirmed =true
        };
        Dictionary<string,string> userClaims =new(){
            { TokenKey.DisplayName , mockUser.UserName},
            { TokenKey.UserId , mockUser.Id.ToString() } ,
            { TokenKey.Id , Guid.NewGuid().ToString() },
            { TokenKey.IssuerAt , DateTime.UtcNow.ToString() } ,
            { TokenKey.Issuer , "test_issuer" } ,
            { TokenKey.Audience , "test_audience" } ,
            { TokenKey.ExpireAt , DateTime.UtcNow.AddMinutes(60).ToString() },
            {TokenKey.IsBlocked , false.ToString() } ,
            {TokenKey.Reason , "OK" } ,
            { TokenKey.NotBefore , DateTime.UtcNow.ToString() }
        };
        return (mockUser, userClaims);
    }
      
    // shared for login
    private async Task Login_Shared(string loginName , AppUser mockUser , Dictionary<string,string> userClaims) {
        //Arrange
        string password = "AppUser123/";   

        _mocker.GetMock<UserManager<AppUser>>()
            .Setup(x => x.CheckPasswordAsync(mockUser , It.IsAny<string>()))
            .ReturnsAsync(true);

        _mocker.GetMock<SignInManager<AppUser>>()
            .Setup(x => x.PasswordSignInAsync(mockUser , It.IsAny<string>() , true , true))
            .ReturnsAsync(SignInResult.Success);

        _mocker.GetMock<IClaimsGenerator>()
            .Setup(x => x.CreateRegularClaims(mockUser.Id , mockUser.UserName))
            .Returns(userClaims);

        _mocker.GetMock<IAuthTokenService>()
          .Setup(x => x.GenerateAsync(It.IsAny<Dictionary<string , string>>() , null))
          .ReturnsAsync(new AccountResult("jwt_token" , userClaims));


        //Act
        var accountResult = await _accountManager.LoginAsync(loginName, password, true, true);

        //Assert       
        mockUser.EmailConfirmed.Should().BeTrue();
        accountResult.Should().NotBeNull();
        accountResult.AuthToken.Should().NotBeNullOrWhiteSpace();
        var claims =  accountResult.KeyValueClaims;
        claims.Should().NotBeNull().And.HaveCount(10);

        _mocker.Verify<SignInManager<AppUser>>(x => x.PasswordSignInAsync(mockUser , It.IsAny<string>() , true , true) , Times.Once);
        _mocker.Verify<UserManager<AppUser>>(x => x.CheckPasswordAsync(mockUser , It.IsAny<string>()) , Times.Once);
        _mocker.Verify<IAuthTokenService>(x => x.GenerateAsync(It.IsAny<Dictionary<string , string>>() , null) , Times.Once);
        _mocker.Verify<IClaimsGenerator>(x => x.CreateRegularClaims(It.IsAny<AppUserId>() , It.IsAny<string>()) , Times.Once);

    }

}
