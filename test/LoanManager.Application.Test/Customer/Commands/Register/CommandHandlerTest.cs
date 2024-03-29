using AutoFixture;
using FluentAssertions;
using LoanManager.Application.Customer.Commands.Register;
using LoanManager.Application.Test.Customer.Commands.Common;
using LoanManager.Domain.Common.Errors;
using Moq;

namespace LoanManager.Application.Test.Customer.Commands.Register;

public class CommandHandlerTest : BaseHandler
{
    private readonly CommandHandler _handler;

    public CommandHandlerTest()
    {
        _handler = new CommandHandler(_customerRepositoryMock.Object);
    }
    
    private void GivenCustomerAddRepository() 
        => _customerRepositoryMock.Setup(x => x.Add(It.IsAny<Domain.Entities.Customer>()));
    
    private void ThenAddWasCalled() 
        => _customerRepositoryMock.Verify(x => x.Add(It.IsAny<Domain.Entities.Customer>()), Times.Once);
    
    [Fact]
    public async Task Should_Fail_With_Duplicated_Phone()
    {
        // arrange
        GivenCustomerExistsPhoneNumberRepository();
        var command = _fixture.Create<Command>();
        
        // act
        var sut = await _handler.Handle(command, default);
        
        // assert
        sut.IsError.Should().BeTrue();
        sut.Errors.First().Should().Be(Errors.Customer.DuplicatedPhone);
        ThenExistsByPhoneWasCalled();
    }

    [Fact]
    public async Task Should_Succeed_With_Not_Found_Customer_PhoneNumber()
    {
        // arrange
        GivenCustomerNotExistsByPhoneNumberRepository();
        GivenCustomerAddRepository();
        var command = _fixture.Create<Command>();

        // act
        var sut = await _handler.Handle(command, default);

        // assert
        sut.IsError.Should().BeFalse();
        sut.Value.Customer.Should().NotBeNull()
            .And.BeEquivalentTo(command);
        ThenExistsByPhoneWasCalled();
        ThenAddWasCalled();
    }
}