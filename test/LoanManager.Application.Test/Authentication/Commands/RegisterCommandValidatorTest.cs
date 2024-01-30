using FluentAssertions;

using LoanManager.Application.Authentication.Commands.Register;

namespace LoanManager.Application.Test.Authentication.Commands;

public class RegisterCommandValidatorTest
{
    private readonly CommandValidator _validator = new();

    [Fact]
    public void Should_Fail_With_Empty_FirstName()
    {
        //arrange
        var registerCommand = RegisterCommandFaker.Fake() with
        {
            FirstName = String.Empty,
        };

        //act
        var sut = _validator.Validate(registerCommand);

        //assert
        sut.Should().NotBeNull();
        sut.Errors.Should().HaveCount(1);
        sut.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void Should_Fail_With_Empty_LastName()
    {
        //arrange
        var registerCommand = RegisterCommandFaker.Fake() with
        {
            LastName = String.Empty
        };

        //act
        var sut = _validator.Validate(registerCommand);

        //assert
        sut.Should().NotBeNull();
        sut.Errors.Should().HaveCount(1);
        sut.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void Should_Fail_With_Empty_Password()
    {
        //arrange
        var registerCommand = RegisterCommandFaker.Fake() with
        {
            Password = String.Empty,
        };

        //act
        var sut = _validator.Validate(registerCommand);

        //assert
        sut.Should().NotBeNull();
        sut.Errors.Should().HaveCount(1);
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

        //act
        var sut = _validator.Validate(registerCommand);

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

        //act
        var sut = _validator.Validate(registerCommand);

        //assert
        sut.Should().NotBeNull();
        sut.Errors.Should().BeEmpty();
        sut.IsValid.Should().BeTrue();
    }
}