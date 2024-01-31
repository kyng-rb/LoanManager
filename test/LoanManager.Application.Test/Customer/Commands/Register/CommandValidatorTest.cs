using Bogus;
using FluentAssertions;
using LoanManager.Application.Customer.Commands.Register;

namespace LoanManager.Application.Test.Customer.Commands.Register;

public class CommandValidatorTest
{
    private readonly CommandValidator _validator = new();
    private readonly Faker _faker = new();

    [Fact]
    public async Task Should_Fail_With_Empty_Name()
    {
        // arrange
        var command = CommandFaker.Command() with
        {
            FirstName = string.Empty
        };

        // act
        var sut = await _validator.ValidateAsync(command);

        // assert
        sut.Should().NotBeNull();
        sut.IsValid.Should().BeFalse();
        sut.Errors.Count.Should().Be(1);
        sut.Errors.First().ErrorMessage.Should().Be("FirstName is required.");
    }
    
    [Fact]
    public async Task Should_Fail_With_Empty_Phone()
    {
        // arrange
        var command = CommandFaker.Command() with
        {
            Phone = string.Empty
        };

        // act
        var sut = await _validator.ValidateAsync(command);

        // assert
        sut.Should().NotBeNull();
        sut.IsValid.Should().BeFalse();
        sut.Errors.First().ErrorMessage.Should().Be("Phone Number is required.");
    }
    
    [Fact]
    public async Task Should_Fail_With_Invalid_PhoneNumber()
    {
        // arrange
        var command = CommandFaker.Command() with
        {
            Phone = _faker.Phone.PhoneNumber("####YYYY")
        };

        // act
        var sut = await _validator.ValidateAsync(command);

        // assert
        sut.Should().NotBeNull();
        sut.IsValid.Should().BeFalse();
        sut.Errors.Count.Should().Be(3);
        
        sut.Errors.First().ErrorMessage.Should().Be("PhoneNumber must have the following format ########");
    }
    
    [Fact]
    public async Task Should_Fail_With_Invalid_Characters_Length_PhoneNumber()
    {
        // arrange
        var command = CommandFaker.Command() with
        {
            Phone = _faker.Phone.PhoneNumber("####")
        };

        // act
        var sut = await _validator.ValidateAsync(command);

        // assert
        sut.Should().NotBeNull();
        sut.IsValid.Should().BeFalse();
        sut.Errors.Count.Should().Be(2);
        sut.Errors.First().ErrorMessage.Should().Be("PhoneNumber must have 8 characters.");
    }
    
    [Fact]
    public async Task Should_Succeed_With_Valid_Input()
    {
        // arrange
        var command = CommandFaker.Command();

        // act
        var sut = await _validator.ValidateAsync(command);

        // assert
        sut.Should().NotBeNull();
        sut.IsValid.Should().BeTrue();
        sut.Errors.Count.Should().Be(0);
    }
}