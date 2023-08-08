using FluentAssertions;

using LoanManager.Application.Authentication.Commands.Register;

namespace LoanManager.Application.Tests.Authentication.Commands;

public class RegisterCommandValidatorTest
{
    [Fact]
    public void Should_Fail_With_Empty_Inputs()
    {
        //arrange
        var registerCommand = RegisterCommandFaker.Fake() with
        {
            Password = String.Empty,
            FirstName = String.Empty,
            LastName = String.Empty
        };

        var validator = new RegisterCommandValidator();

        //act
        var sut = validator.Validate(registerCommand);

        //assert
        sut.Should().NotBeNull();
        sut.Errors.Should().HaveCount(3);
        sut.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Fail_With_Invalid_Email()
    {
        //arrange
        var registerCommand = RegisterCommandFaker.Fake() with
        {
            Email = "A sexy string"
        };

        var validator = new RegisterCommandValidator();

        //act
        var sut = validator.Validate(registerCommand);

        //assert
        sut.Should().NotBeNull();
        sut.Errors.Should().HaveCount(1);
        sut.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Be_Ok_With_Valid_Input()
    {
        //arrange
        var registerCommand = RegisterCommandFaker.Fake();

        var validator = new RegisterCommandValidator();

        //act
        var sut = validator.Validate(registerCommand);

        //assert
        sut.Should().NotBeNull();
        sut.Errors.Should().BeEmpty();
        sut.IsValid.Should().BeTrue();
    }
}