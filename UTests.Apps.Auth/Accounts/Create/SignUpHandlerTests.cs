//using Apps.Auth.Abstractions;
//using Domains.Auth.AppUserEntity.Aggregate;
//using FluentAssertions;
//using Moq;
//using Shared.Auth.Enums;
//using Shared.Auth.Models;

//namespace UTests.Apps.Auth.Accounts.Create;
//public class SignUpHandlerTests {

//    private readonly Mock<IAccountUOW> _unitOfWork;


//    public SignUpHandlerTests() {
//        _unitOfWork = new Mock<IAccountUOW>();
//    }


//    [Fact]
//    public async Task SignUp_Should_Return_Success_AccountResult() {

//        //Arrange
//        var model = new SignUpModel{
//            Email = "Masoud2@gmail.com" ,
//            UserName = "Masoud2" ,
//            Password = "Masoud123/" ,
//            BirthDate = DateTime.UtcNow,
//            Gender = Gender.Male
//        };
//        var cancelationToken = new CancellationToken();

//        var appUser = AppUser.Create(model.UserName,model.Email);
//        AppUser? findUser = null;
//        _unitOfWork.Setup(q => q.Queries.FindByEmailAsync(model.Email)).ReturnsAsync(findUser);
//        _unitOfWork.Setup(q => q.Queries.FindByUserNameAsync(model.UserName)).ReturnsAsync(findUser);

//        var expectedAccountResult = new AccountResult(ResultStatus.Succeed,"jwt-token",new());
//       // _unitOfWork.Setup(c => c.Commands.SignUpAsync(It.IsAny<AppUser>() , model.Password)) .ReturnsAsync(expectedAccountResult);

//        //Act
//        var handler = new SignUpHandler(_unitOfWork.Object);
//        var accountResult = await handler.Handle(model,cancelationToken);

//        //Assert

//        findUser.Should().BeNull();
//        _unitOfWork.Verify(q => q.Queries.FindByEmailAsync(model.Email) , Times.Once);
//        _unitOfWork.Verify(q => q.Queries.FindByUserNameAsync(model.UserName) , Times.Once);
//      //  _unitOfWork.Verify(c => c.Commands.SignUpAsync(It.IsAny<AppUser>() , model.Password) , Times.Once);

//        accountResult.Should().NotBeNull();
//        accountResult.ResultStatus.Should().Be(ResultStatus.Succeed);
//        accountResult.AuthToken.Should().NotBeNullOrWhiteSpace();
//        accountResult.KeyValueClaims.Should().NotBeNull();

//    }

//}
