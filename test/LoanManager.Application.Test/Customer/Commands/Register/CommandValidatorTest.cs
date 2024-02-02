using Bogus;
using FluentAssertions;
using LoanManager.Application.Customer.Commands.Register;
using LoanManager.Application.Test.Customer.Commands.Common;

namespace LoanManager.Application.Test.Customer.Commands.Register;

public class CommandValidatorTest
{
    private readonly CommandValidator _validator = new();
    private readonly Faker _faker = new();

    private const string RequiredPhoneNumberMessage = "Phone Number is required.";
    private const string InvalidPhoneNumberLengthMessage = "PhoneNumber must have 8 characters.";
    private const string RequiredFirstNameMessage = "FirstName is required.";
    private const string InvalidPhoneNumberFormatMessage = "PhoneNumber must have the following format ########";

    [Fact]
    public async Task Should_Fail_With_Empty_Name()
    {
        // arrange
        var command = CommandFaker.RegisterCommand() with
        {
            FirstName = string.Empty
        };

        // act
        var sut = await _validator.ValidateAsync(command);

        // assert
        sut.Should().NotBeNull();
        sut.IsValid.Should().BeFalse();
        sut.Errors.Count.Should().Be(1);
        sut.Errors.First().ErrorMessage.Should().Be(RequiredFirstNameMessage);
    }
    
    [Fact]
    public async Task Should_Fail_With_Empty_Phone()
    {
        // arrange
        var command = CommandFaker.RegisterCommand() with
        {
            Phone = string.Empty
        };
        
        var expectedErrorMessages = new string[]
        {
            RequiredPhoneNumberMessage,
            InvalidPhoneNumberFormatMessage,
            InvalidPhoneNumberLengthMessage
        };

        // act
        var sut = await _validator.ValidateAsync(command);

        // assert
        sut.Should().NotBeNull();
        sut.IsValid.Should().BeFalse();
        sut.Errors.Count.Should().Be(3);
        sut.Errors.ForEach(x =>
        {
            expectedErrorMessages.Should().Contain(x.ErrorMessage);
        });
    }
    
    [Fact]
    public async Task Should_Fail_With_Invalid_Format_PhoneNumber()
    {
        // arrange
        var command = CommandFaker.RegisterCommand() with
        {
            Phone = _faker.Phone.PhoneNumber("#######Y")
        };
        
        // act
        var sut = await _validator.ValidateAsync(command);

        // assert
        sut.Should().NotBeNull();
        sut.IsValid.Should().BeFalse();
        sut.Errors.Count.Should().Be(1);
        sut.Errors.First(x =>
            x.ErrorMessage.Equals(InvalidPhoneNumberFormatMessage, StringComparison.InvariantCultureIgnoreCase));
    }
    
    [Fact]
    public async Task Should_Fail_With_Invalid_Character_Length_PhoneNumber()
    {
        // arrange
        var command = CommandFaker.RegisterCommand() with
        {
            Phone = _faker.Phone.PhoneNumber("####")
        };

        var expectedErrorMessages = new string[]
        {
            InvalidPhoneNumberFormatMessage,
            InvalidPhoneNumberLengthMessage
        };
        
        // act
        var sut = await _validator.ValidateAsync(command);

        // assert
        sut.Should().NotBeNull();
        sut.IsValid.Should().BeFalse();
        sut.Errors.Count.Should().Be(2);
        sut.Errors.ForEach(x =>
        {
            expectedErrorMessages.Should().Contain(x.ErrorMessage);
        });
    }
    
    [Fact]
    public async Task Should_Succeed_With_Valid_Input()
    {
        // arrange
        var command = CommandFaker.RegisterCommand();

        // act
        var sut = await _validator.ValidateAsync(command);

        // assert
        sut.Should().NotBeNull();
        sut.IsValid.Should().BeTrue();
        sut.Errors.Count.Should().Be(0);
    }
}