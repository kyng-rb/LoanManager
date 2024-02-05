using FluentAssertions;
using FluentValidation.TestHelper;
using LoanManager.Application.Customer.Commands.Update;
using LoanManager.Application.Test.Customer.Commands.Common;

namespace LoanManager.Application.Test.Customer.Commands.Update;

public class CommandValidatorTest : BaseHandler
{
    private readonly CommandValidator _validator = new();
    
    private const string RequiredPhoneNumberMessage = "Phone Number is required.";
    private const string InvalidPhoneNumberLengthMessage = "PhoneNumber must have 8 characters.";
    private const string RequiredFirstNameMessage = "FirstName is required.";
    private const string InvalidPhoneNumberFormatMessage = "PhoneNumber must have the following format ########";
    private const string InvalidIdentifierMessage = "Invalid Customer Id value";
    
    [Fact]
    public async Task Should_Fail_With_Default_Identifier()
    {
        // arrange
        var command = CommandFaker.UpdateCommand() with
        {
            CustomerId = default
        };

        // act
        var sut = await _validator.TestValidateAsync(command);

        // assert
        sut.ShouldHaveAnyValidationError();
        sut.ShouldHaveValidationErrorFor(x => x.CustomerId)
            .WithErrorMessage(InvalidIdentifierMessage);
    }
    
    [Fact]
    public async Task Should_Fail_With_Empty_Name()
    {
        // arrange
        var command = CommandFaker.UpdateCommand() with
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
        var command = CommandFaker.UpdateCommand() with
        {
            Phone = string.Empty
        };
        
        var expectedErrorMessages = new []
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
        var command = CommandFaker.UpdateCommand() with
        {
            Phone = _faker.Phone.PhoneNumber("#######Y")
        };
        
        // act
        var sut = await _validator.ValidateAsync(command);

        // assert
        sut.Should().NotBeNull();
        sut.IsValid.Should().BeFalse();
        sut.Errors.Count.Should().Be(1);
        _ = sut.Errors.First(x =>
            x.ErrorMessage.Equals(InvalidPhoneNumberFormatMessage, StringComparison.InvariantCultureIgnoreCase));
    }
    
    [Fact]
    public async Task Should_Fail_With_Invalid_Character_Length_PhoneNumber()
    {
        // arrange
        var command = CommandFaker.UpdateCommand() with
        {
            Phone = _faker.Phone.PhoneNumber("####")
        };

        var expectedErrorMessages = new []
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
}