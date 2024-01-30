using FluentAssertions;

using LoanManager.Application.Authentication.Queries.Login;

namespace LoanManager.Application.Test.Authentication.Queries;

public class LoginQueryValidatorTest
{
    [Fact]
    public void Should_Fail_With_Empty_Inputs()
    {
        //arrange
        var loginQuery = LoginQueryFaker.Fake() with
        {
            Email = String.Empty
        };

        var validator = new LoginQueryValidator();

        //act
        var sut = validator.Validate(loginQuery);

        //assert
        sut.Should().NotBeNull();
        sut.Errors.Should().HaveCount(1);
        sut.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Fail_With_Invalid_Email()
    {
        //arrange
        var loginQuery = LoginQueryFaker.Fake() with
        {
            Email = "A sexy string"
        };

        var validator = new LoginQueryValidator();

        //act
        var sut = validator.Validate(loginQuery);

        //assert
        sut.Should().NotBeNull();
        sut.Errors.Should().HaveCount(1);
        sut.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Be_Ok_With_Valid_Input()
    {
        //arrange
        var loginQuery = LoginQueryFaker.Fake();

        var validator = new LoginQueryValidator();

        //act
        var sut = validator.Validate(loginQuery);

        //assert
        sut.Should().NotBeNull();
        sut.Errors.Should().BeEmpty();
        sut.IsValid.Should().BeTrue();
    }
}