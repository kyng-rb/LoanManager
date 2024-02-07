using FluentAssertions;
using FluentValidation.TestHelper;
using LoanManager.Application.Customer.Commands.Update;
using LoanManager.Application.Test.Customer.Commands.Common;

namespace LoanManager.Application.Test.Customer.Commands.Update;

public class CommandValidatorTest : BaseHandler
{
    private readonly CommandValidator _validator = new();
    
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
        sut.ShouldHaveValidationErrorFor(x => x.CustomerId)
            .WithErrorMessage(InvalidIdentifierMessage)
            .Only();
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
        var sut = await _validator.TestValidateAsync(command);

        // assert
        sut.ShouldHaveValidationErrorFor(x => x.FirstName)
            .WithErrorMessage(RequiredFirstNameMessage)
            .Only();
    }

    [Fact]
    public async Task Should_Succeed_With_FirstName_LastName_PhoneNumber_CustomerId()
    {
        // arrange
        var command = CommandFaker.UpdateCommand();
        
        // act
        var sut = await _validator.TestValidateAsync(command, default);

        // assert
        sut.ShouldNotHaveAnyValidationErrors();
        sut.IsValid.Should().BeTrue();
    }
}