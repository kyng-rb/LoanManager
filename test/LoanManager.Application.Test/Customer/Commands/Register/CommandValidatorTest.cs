using FluentAssertions;
using FluentValidation.TestHelper;
using LoanManager.Application.Customer.Commands.Register;
using LoanManager.Application.Test.Customer.Commands.Common;

namespace LoanManager.Application.Test.Customer.Commands.Register;

public class CommandValidatorTest : BaseHandler
{
    private readonly CommandValidator _validator = new();

    [Fact]
    public async Task Should_Fail_With_Empty_Name()
    {
        // arrange
        var command = CommandFaker.RegisterCommand() with
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
    public async Task Should_Succeed_With_FirstName_LastName_And_PhoneNumber()
    {
        // arrange
        var command = CommandFaker.RegisterCommand();

        // act
        var sut = await _validator.TestValidateAsync(command);

        // assert
        sut.ShouldNotHaveAnyValidationErrors();
        sut.IsValid.Should().BeTrue();
    }
}