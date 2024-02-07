using FluentAssertions;
using FluentValidation.TestHelper;
using LoanManager.Application.Customer.Common;

namespace LoanManager.Application.Test.Customer.Commands.Common;

public class NicaraguaPhoneNumberValidatorTest : BaseHandler
{
    private readonly NicaraguaPhoneNumberValidator _validator = new();
    
    [Fact]
    public async Task Should_Fail_With_Empty_Phone()
    {
        // arrange
        string phoneNumber = string.Empty;

        // act
        var sut = await _validator.TestValidateAsync(phoneNumber);

        // assert
        sut.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage(RequiredPhoneNumberMessage);
        sut.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage(InvalidPhoneNumberFormatMessage);
        sut.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage(InvalidPhoneNumberLengthMessage);
    }
    
    [Fact]
    public async Task Should_Fail_With_Invalid_Format_PhoneNumber()
    {
        // arrange
        string phone = _faker.Phone.PhoneNumber("#######Y");
        
        // act
        var sut = await _validator.TestValidateAsync(phone);

        // assert
        sut.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage(InvalidPhoneNumberFormatMessage)
            .Only();
    }
    
    [Fact]
    public async Task Should_Fail_With_Invalid_Character_Length_PhoneNumber()
    {
        // arrange
        string phone = _faker.Phone.PhoneNumber("####");
        
        // act
        var sut = await _validator.TestValidateAsync(phone);

        // assert
        sut.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage(InvalidPhoneNumberFormatMessage);
        sut.ShouldHaveValidationErrorFor(x => x)
            .WithErrorMessage(InvalidPhoneNumberLengthMessage);
    }

    [Fact]
    public async Task Should_Succeed_With_Correct_Format_PhoneNumber()
    {
        // arrange
        string phone = _faker.Phone.PhoneNumber("########");
        
        // act
        var sut = await _validator.TestValidateAsync(phone);

        // assert
        sut.ShouldNotHaveAnyValidationErrors();
        sut.IsValid.Should().BeTrue();
    }
}