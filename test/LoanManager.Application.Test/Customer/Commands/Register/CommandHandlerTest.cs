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

    private void Given_Add_CustomerRepository()
        => _customerRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Domain.CustomerAggregate.Customer>(), default))
            .ReturnsAsync((Domain.CustomerAggregate.Customer customer, CancellationToken token) => customer);
    
    private void Then_Add_CustomerRepository_Was_Called() 
        => _customerRepositoryMock
            .Verify(x => x.AddAsync(It.IsAny<Domain.CustomerAggregate.Customer>(), default), Times.Once);
    
    [Fact]
    public async Task Should_Fail_With_Duplicated_Phone()
    {
        // arrange
        Given_Found_By_PhoneNumber_CustomerRepository();
        var command = _fixture.Create<Command>();
        
        // act
        var sut = await _handler.Handle(command, default);
        
        // assert
        sut.IsError.Should().BeTrue();
        sut.Errors.First().Should().Be(Errors.Customer.DuplicatedPhone);
        Then_Any_By_Phone_Was_Called();
    }

    [Fact]
    public async Task Should_Succeed_With_Not_Found_Customer_PhoneNumber()
    {
        // arrange
        Given_Not_Found_By_PhoneNumber_CustomerRepository();
        Given_Add_CustomerRepository();
        var command = _fixture.Create<Command>();

        // act
        var sut = await _handler.Handle(command, default);

        // assert
        sut.IsError.Should().BeFalse();
        sut.Value.Customer.Should().NotBeNull()
            .And.BeEquivalentTo(command);
        Then_Any_By_Phone_Was_Called();
        Then_Add_CustomerRepository_Was_Called();
    }
}