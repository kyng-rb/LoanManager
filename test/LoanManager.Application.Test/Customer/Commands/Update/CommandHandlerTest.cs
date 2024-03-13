using AutoFixture;
using FluentAssertions;
using LoanManager.Application.Customer.Commands.Update;
using LoanManager.Application.Test.Customer.Commands.Common;
using LoanManager.Domain.Common.Errors;
using LoanManager.Domain.CustomerAggregate.Specifications;
using Moq;

namespace LoanManager.Application.Test.Customer.Commands.Update;

public class CommandHandlerTest : BaseHandler
{
    private readonly CommandHandler _handler;

    public CommandHandlerTest()
    {
        _handler = new CommandHandler(_customerRepositoryMock.Object);
    }

    private void Given_Found_ById_CustomerRepository() 
        => _customerRepositoryMock
            .Setup(x => x.AnyAsync(It.IsAny<CustomerById>(), default)).ReturnsAsync(true);

    private void Given_Update_CustomerRepository()
        => _customerRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<Domain.CustomerAggregate.Customer>(), default))
            .Returns(Task.CompletedTask);
    
    private void Then_Update_CustomerRepository_Was_Called()
        => _customerRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.CustomerAggregate.Customer>(), default), Times
        .Once);

    [Fact]
    public async Task Should_Fail_With_Customer_Not_Found()
    {
        // arrange
        Given_Not_Found_By_Id_CustomerRepository();
        var command = _fixture.Create<Command>();
        
        // act
        var sut = await _handler.Handle(command, default);

        // assert
        sut.IsError.Should().BeTrue();
        sut.Errors.Count.Should().Be(1);
        sut.Errors.First().Should().Be(Errors.Customer.NotFound);
        Then_Any_By_Id_Was_Called();
    }
    
    [Fact]
    public async Task Should_Fail_With_Found_Customer_And_Already_Used_Phone()
    {
        // arrange
        Given_Found_By_PhoneNumber_CustomerRepository();
        Given_Found_ById_CustomerRepository();
        var command = _fixture.Create<Command>();
        
        // act
        var sut = await _handler.Handle(command, default);

        // assert
        sut.IsError.Should().BeTrue();
        sut.Errors.Count.Should().Be(1);
        sut.Errors.First().Should().Be(Errors.Customer.DuplicatedPhone);
        Then_Any_By_Id_Was_Called();
        Then_Any_By_Phone_Was_Called();
    }
    
    [Fact]
    public async Task Should_Succeed_With_Found_User_And_Not_Used_PhoneNumber()
    {
        // arrange
        Given_Found_ById_CustomerRepository();
        Given_Not_Found_By_PhoneNumber_CustomerRepository();
        Given_Update_CustomerRepository();
        var command = _fixture.Create<Command>();

        // act
        var sut = await _handler.Handle(command, default);

        //assert
        sut.IsError.Should().BeFalse();
        sut.Value.Customer.Should().NotBeNull()
            .And.BeEquivalentTo(command, option
                => option.WithMapping<Domain.CustomerAggregate.Customer>(
                    s => s.CustomerId,
                    x => x.Id));
        
        Then_Any_By_Id_Was_Called();
        Then_Any_By_Phone_Was_Called();
        Then_Update_CustomerRepository_Was_Called();
    }
}